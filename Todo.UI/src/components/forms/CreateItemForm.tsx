import { useFormik } from "formik";
import FilledButton from "../filledButton";
import { Form, Input, InputGroup, Label, Select, TextArea } from "./styles";
import { useQueryClient } from "react-query";
import { createItem } from "../../services/api/itens";
import { Item } from "../../types/item";

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
  onSucess: () => void;
  columns?: { label: string; value: string }[];
};
export default function CreateItemForm({
  onSucess,
  boardId,
  columns,
}: CreateItemFormProps) {
  const client = useQueryClient();
  const formik = useFormik({
    initialValues: {
      title: "",
      description: "",
      priority: 0,
      columnId: columns ? columns[0].value : "",
      boardId: "",
    },
    onSubmit: async (values) => {
      if (boardId) {
        values["boardId"] = boardId;
      }
      const data = await createItem(values);

      client.setQueryData<Item[]>(["itens"], (previous) => {
        if (!previous) {
          return [data];
        }
        return [...previous, data];
      });

      if (boardId) {
        client.invalidateQueries(["board", boardId]);
      }

      onSucess();
    },
  });

  return (
    <Form onSubmit={formik.handleSubmit} width="30vw">
      <InputGroup>
        <Label>Título</Label>
        <Input
          name="title"
          onChange={formik.handleChange}
          value={formik.values.title}
        />
      </InputGroup>
      <InputGroup>
        <Label>Descrição</Label>
        <TextArea
          rows={10}
          name="description"
          onChange={formik.handleChange}
          value={formik.values.description}
        />
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
      <FilledButton>Criar</FilledButton>
    </Form>
  );
}