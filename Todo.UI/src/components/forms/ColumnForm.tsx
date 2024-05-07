import { useState } from "react";
import { createColumn, editColumn } from "@/services/api/column";
import { useQueryClient } from "@tanstack/react-query";
import { ExpandedBoard } from "@/types/board";
import * as Yup from "yup";
import { toast } from "react-toastify";
import { produce } from "immer";
import Form from "../form";

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

  const validationSchema = Yup.object().shape({
    name: Yup.string()
      .required("O nome da coluna é obrigatório!")
      .min(5, "O nome deve ter no mínimo 5 caracteres!"),
    type: Yup.number().min(0, "Tipo inválido!").max(2, "Tipo inválido!"),
  });

  async function handleSubmit(values: { type: string; name: string }) {
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

            const columnId = prev.columns.findIndex((x) => x.id === column.id);
            prev.columns[columnId].name = column.name;

            return prev;
          })
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
          })
        );
      }
      onSuccess();
    } catch (e) {
      setLoading(false);
      toast.error("Oops, ocorreu um erro, tente novamente mais tarde!");
    }
  }

  return (
    <Form
      initialValues={{
        name: data?.name || "",
        type: data?.type || "0",
      }}
      onSubmit={handleSubmit}
      validationSchema={validationSchema}
      width="30vw"
      title={`${data ? "Editar" : "Criar"} coluna`}
    >
      <Form.Input label="Título" name="name" />
      <Form.Select
        label="Tipo de coluna"
        name="type"
        options={[
          { label: "Aberto", value: "0" },
          { label: "Em andamento", value: "1" },
          { label: "Fechado", value: "2" },
        ]}
      />
      <Form.Submit label={data ? "Editar" : "Criar"} $loading={loading} />
    </Form>
  );
}
