import { useState } from "react";
import { useFormik } from "formik";
import FilledButton from "../filledButton";
import { Form, Input, InputGroup, Label, Select } from "./styles";
import { createColumn, editColumn } from "@/services/api/column";
import { useQueryClient } from "@tanstack/react-query";
import { ExpandedBoard } from "@/types/board";
import * as Yup from "yup";
import ErrorMessage from "@/components/forms/ErrorMessage";
import { H1 } from "@/assets/css/global.styles";
import { toast } from "react-toastify";
import { produce } from "immer";

type ColumnFormProps = {
  data?: {
    id: string;
    name: string;
    type: string;
  };
  boardId: string;
  onSuccess: () => void;
};
export default function ColumnForm({
  data,
  onSuccess,
  boardId,
}: ColumnFormProps) {
  const [loading, setLoading] = useState(false);
  const client = useQueryClient();

  const formik = useFormik({
    initialValues: {
      name: data?.name || "",
      type: data?.type || "0",
    },
    validationSchema: Yup.object().shape({
      name: Yup.string()
        .required("O nome da coluna é obrigatório!")
        .min(5, "O nome deve ter no mínimo 5 caracteres!"),
      type: Yup.number().min(0, "Tipo inválido!").max(2, "Tipo inválido!"),
    }),
    onSubmit: async (values) => {
      setLoading(true);
      try {
        if (data) {
          const column = await editColumn({
            id: data.id,
            name: values.name,
            type: parseInt(values.type),
          });
          toast.success("Coluna atualizado com sucesso!");

          client.setQueryData<ExpandedBoard>(
            ["board", boardId],
            produce((prev) => {
              if (!prev) {
                throw new Error("Cache inválido");
              }

              const columnId = prev.columns.findIndex(
                (x) => x.id === column.id,
              );
              prev.columns[columnId].name = column.name;

              return prev;
            }),
          );
        } else if (boardId) {
          const column = await createColumn({
            name: values.name,
            boardId: boardId,
            type: parseInt(values.type),
          });
          toast.success("Coluna atualizada com sucesso!");

          client.setQueryData<ExpandedBoard>(
            ["board", boardId],
            produce((prev) => {
              if (!prev) {
                throw new Error("Cache inválido");
              }
              column.itemCount = 0;

              prev.columns.push(column);

              return prev;
            }),
          );
        }
        onSuccess();
      } catch (e) {
        setLoading(false);
        toast.error("Oops, ocorreu um erro, tente novamente mais tarde!");
      }
    },
  });

  return (
    <Form onSubmit={formik.handleSubmit} width="30vw">
      <H1>{data ? "Editar" : "Criar"} coluna</H1>
      <InputGroup>
        <Label>Titulo</Label>
        <Input
          type="text"
          name="name"
          value={formik.values.name}
          onChange={formik.handleChange}
        />
        {formik.errors.name && (
          <ErrorMessage>{formik.errors.name}</ErrorMessage>
        )}
      </InputGroup>
      <InputGroup>
        <Label>Tipo de coluna</Label>
        <Select
          name="type"
          value={formik.values.type}
          onChange={formik.handleChange}
        >
          <option value="0">Aberto</option>
          <option value="1">Em andamento</option>
          <option value="2">Fechado</option>
        </Select>
        {formik.errors.type && (
          <ErrorMessage>{formik.errors.type}</ErrorMessage>
        )}
      </InputGroup>
      <FilledButton $loading={loading} type="submit">
        Salvar
      </FilledButton>
    </Form>
  );
}
