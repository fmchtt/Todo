import { useQueryClient } from "@tanstack/react-query";
import { createItem } from "@/services/api/itens";
import { CreateItemProps, Item } from "@/types/item";
import { ExpandedBoard } from "@/types/board";
import * as Yup from "yup";
import { toast } from "react-toastify";
import { produce } from "immer";
import Form from "../form";
import { useState } from "react";

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
  const [loading, setLoading] = useState(false);

  const validationSchema = Yup.object().shape({
    title: Yup.string()
      .required("O nome da tarefa é obrigatório!")
      .min(5, "O nome deve ter no mínimo 5 caracteres!"),
    description: Yup.string()
      .required("A descrição da tarefa é obrigatório!")
      .min(10, "A descrição deve ter no mínimo 10 caracteres!"),
    priority: Yup.number()
      .required("A prioridade é obrigatória!")
      .min(0, "Prioridade inválida!"),
  });

  async function handleSubmit(values: {
    title: string;
    description: string;
    priority: string;
    columnId: string;
    boardId: string;
  }) {
    {
      setLoading(true);

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
          })
        );

        if (boardId) {
          client.setQueryData<ExpandedBoard>(
            ["board", boardId],
            produce((prev) => {
              if (!prev) {
                return;
              }

              const colIdx = prev.columns.findIndex(
                (x) => x.id === values.columnId
              );
              if (colIdx >= 0) {
                prev.columns[colIdx].itens.push(data);
                prev.columns[colIdx].itemCount += 1;
              }

              return prev;
            })
          );
        }

        onSuccess();
      } catch (e) {
        toast.error("Oops, ocorreu um erro, tente novamente mais tarde!");
      } finally {
        setLoading(false);
      }
    }
  }

  return (
    <Form
      initialValues={{
        title: "",
        description: "",
        priority: "0",
        columnId: columns?.at(0)?.value || "",
        boardId: "",
      }}
      onSubmit={handleSubmit}
      validationSchema={validationSchema}
    >
      <Form.Input name="title" label="Título" placeholder="ex: Item 1" />
      <Form.Editor name="description" label="Descrição" />
      <Form.Select
        name="priority"
        label="Prioridade"
        options={priorityChoices}
      />
      {columns && (
        <Form.Select label="Coluna" name="column" options={columns} />
      )}
      <Form.Submit label="Criar" $loading={loading} />
    </Form>
  );
}
