import * as Yup from "yup";
import { toast } from "react-toastify";
import Form from "../form";
import { useColumnCreate, useColumnUpdate } from "@/adapters/columnAdapters";

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
  const updateColumnMutation = useColumnUpdate({
    onSuccess: () => {
      toast.success("Coluna atualizado com sucesso!");
      onSuccess();
    },
    onError: () => {
      toast.error("Erro ao atualizar coluna, tente novamente mais tarde!");
    },
  });

  const createColumnMutation = useColumnCreate({
    onSuccess: () => {
      toast.success("Coluna criada com sucesso!");
      onSuccess();
    },
    onError: () => {
      toast.error("Erro ao crair coluna, tente novamente mais tarde!");
    },
  });

  const validationSchema = Yup.object().shape({
    name: Yup.string()
      .required("O nome da coluna é obrigatório!")
      .min(5, "O nome deve ter no mínimo 5 caracteres!"),
    type: Yup.number().min(0, "Tipo inválido!").max(2, "Tipo inválido!"),
  });

  async function handleSubmit(values: { type: string; name: string }) {
    if (data) {
      updateColumnMutation.mutate({
        id: data.id,
        name: values.name,
        type: parseInt(values.type),
        boardId: boardId,
      });
    } else {
      createColumnMutation.mutate({
        name: values.name,
        boardId: boardId,
        type: parseInt(values.type),
      });
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
      <Form.Submit
        label={data ? "Editar" : "Criar"}
        $loading={
          createColumnMutation.isPending || updateColumnMutation.isPending
        }
      />
    </Form>
  );
}
