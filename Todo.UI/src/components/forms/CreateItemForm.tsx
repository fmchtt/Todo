import { useFormik } from "formik";
import FilledButton from "../filledButton";
import { Form, Input, InputGroup, Label, Select, TextArea } from "./styles";
import { useQueryClient } from "react-query";
import { createItem } from "@/services/api/itens";
import { CreateItemProps, Item } from "@/types/item";
import { ExpandedBoard } from "@/types/board";

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
      const reqData: CreateItemProps = {
        title: values.title,
        description: values.description,
        priority: values.priority,
      };

      if (boardId) {
        reqData["boardId"] = boardId;
      }
      if (columns) {
        reqData["columnId"] = values.columnId;
      }

      const data = await createItem(reqData);

      client.setQueryData<Item[]>(["itens"], (previous) => {
        if (!previous) {
          return [data];
        }
        return [...previous, data];
      });

      if (boardId) {
        client.setQueryData<ExpandedBoard>(["board", boardId], (prev) => {
          if (!prev) {
            throw new Error("Cache invalido!");
          }

          const colIdx = prev.columns.findIndex(
            (x) => x.id === values.columnId
          );
          if (colIdx >= 0) {
            prev.columns[colIdx].itens.push(data);
            prev.columns[colIdx].itemCount += 1;
          }

          return prev;
        });
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
      <FilledButton type="submit">Criar</FilledButton>
    </Form>
  );
}
