import { useFormik } from "formik";
import FilledButton from "../filledButton";
import { Form, Input, InputGroup, Label, Select } from "./styles";
import { useQueryClient } from "@tanstack/react-query";
import { createItem } from "@/services/api/itens";
import { CreateItemProps, Item } from "@/types/item";
import { ExpandedBoard } from "@/types/board";
import * as Yup from "yup";
import ErrorMessage from "@/components/forms/ErrorMessage";
import { H1 } from "@/assets/css/global.styles";
import { toast } from "react-toastify";
import { Editor } from "@tinymce/tinymce-react";
import { produce } from "immer";

const priorityChoices = [
  {
    label: "Nenhuma",
    value: 0,
  },
  {
    label: "Muito Baixa",
    value: 1,
  },
  {
    label: "Baixa",
    value: 2,
  },
  {
    label: "Media",
    value: 3,
  },
  {
    label: "Alta",
    value: 4,
  },
  {
    label: "Muito Alta",
    value: 5,
  },
];

type CreateItemFormProps = {
  boardId?: string;
  onSuccess: () => void;
  columns?: { label: string; value: string }[];
};
export default function CreateItemForm({
  onSuccess,
  boardId,
  columns,
}: CreateItemFormProps) {
  const client = useQueryClient();
  const formik = useFormik({
    initialValues: {
      title: "",
      description: "",
      priority: "0",
      columnId: columns?.at(0)?.value || "",
      boardId: "",
    },
    validationSchema: Yup.object().shape({
      title: Yup.string()
        .required("O nome da tarefa é obrigatório!")
        .min(5, "O nome deve ter no mínimo 5 caracteres!"),
      description: Yup.string()
        .required("A descrição da tarefa é obrigatório!")
        .min(10, "A descrição deve ter no mínimo 10 caracteres!"),
      priority: Yup.number()
        .required("A prioridade é obrigatória!")
        .min(0, "Prioridade inválida!"),
    }),
    onSubmit: async (values) => {
      const reqData: CreateItemProps = {
        title: values.title,
        description: values.description,
        priority: parseInt(values.priority),
      };

      if (boardId) {
        reqData["boardId"] = boardId;
      }
      if (columns) {
        reqData["columnId"] = values.columnId;
      }

      try {
        const data = await createItem(reqData);
        toast.success("Tarefa adicionada com sucesso!");

        client.setQueryData<Item[]>(
          ["itens"],
          produce((previous) => {
            if (!previous) {
              return [data];
            }
            return previous.unshift(data);
          }),
        );

        if (boardId) {
          client.setQueryData<ExpandedBoard>(
            ["board", boardId],
            produce((prev) => {
              if (!prev) {
                throw new Error("Cache invalido!");
              }

              const colIdx = prev.columns.findIndex(
                (x) => x.id === values.columnId,
              );
              if (colIdx >= 0) {
                prev.columns[colIdx].itens.push(data);
                prev.columns[colIdx].itemCount += 1;
              }

              return prev;
            }),
          );
        }

        onSuccess();
      } catch (e) {
        toast.error("Oops, ocorreu um erro, tente novamente mais tarde!");
      }
    },
  });

  return (
    <Form onSubmit={formik.handleSubmit} width="30vw">
      <H1>Criar tarefa</H1>
      <InputGroup>
        <Label>Título</Label>
        <Input
          name="title"
          onChange={formik.handleChange}
          value={formik.values.title}
        />
        {formik.errors.title && (
          <ErrorMessage>{formik.errors.title}</ErrorMessage>
        )}
      </InputGroup>
      <InputGroup>
        <Label>Descrição</Label>
        <Editor
          tinymceScriptSrc={`${
            import.meta.env.VITE_API_URL
          }/js/tinymce/tinymce.min.js`}
          init={{
            height: 500,
            plugins: [
              "advlist",
              "autolink",
              "lists",
              "link",
              "image",
              "charmap",
              "anchor",
              "searchreplace",
              "visualblocks",
              "code",
              "fullscreen",
              "insertdatetime",
              "media",
              "table",
              "preview",
              "help",
              "wordcount",
            ],
            language: "pt_BR",
            skin: "oxide-dark",
            content_css: "dark",
            promotion: false,
          }}
          value={formik.values.description}
          onEditorChange={(e) => formik.setFieldValue("description", e)}
        />
        {formik.errors.description && (
          <ErrorMessage>{formik.errors.description}</ErrorMessage>
        )}
      </InputGroup>
      <InputGroup>
        <Label>Prioridade</Label>
        <Select
          name="priority"
          onChange={formik.handleChange}
          value={formik.values.priority}
        >
          {priorityChoices.map((choice, idx) => {
            return (
              <option key={idx} value={choice.value}>
                {choice.label}
              </option>
            );
          })}
        </Select>
        {formik.errors.priority && (
          <ErrorMessage>{formik.errors.priority}</ErrorMessage>
        )}
      </InputGroup>
      {columns && (
        <InputGroup>
          <Label>Coluna</Label>
          <Select
            name="columnId"
            onChange={formik.handleChange}
            value={formik.values.columnId}
          >
            {columns.map((column, idx) => {
              return (
                <option key={idx} value={column.value}>
                  {column.label}
                </option>
              );
            })}
          </Select>
        </InputGroup>
      )}
      <FilledButton type="submit">Criar</FilledButton>
    </Form>
  );
}
