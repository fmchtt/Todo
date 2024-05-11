import { CreateItem } from "@/types/item";
import * as Yup from "yup";
import { toast } from "react-toastify";
import Form from "../form";
import { useItemCreate } from "@/adapters/itemAdapters";

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

  const itemCreate = useItemCreate({
    onSuccess: () => {
      toast.success("Tarefa adicionada com sucesso!");
      onSuccess();
    },
    onError: () => {
      toast.error("Tarefa não criada, tente novamente mais tarde!");
    },
  });

  async function handleSubmit(values: {
    title: string;
    description: string;
    priority: string;
    columnId: string;
    boardId: string;
  }) {
    {
      const reqData: CreateItem = {
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

      itemCreate.mutate(reqData);
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
      <Form.Submit label="Criar" $loading={itemCreate.isPending} />
    </Form>
  );
}
