import { useState } from "react";
import { useFormik } from "formik";
import FilledButton from "../filledButton";
import { Form, Input, InputGroup, Label } from "./styles";
import { EditColumn } from "@/types/column";
import { createColumn, editColumn } from "@/services/api/column";
import { useQueryClient } from "react-query";
import { ExpandedBoard } from "@/types/board";
import * as Yup from "yup";
import ErrorMessage from "@/components/forms/ErrorMessage";

type ColumnFormProps = {
  data?: EditColumn;
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
    },
    validationSchema: Yup.object().shape({
      name: Yup.string()
        .required("O nome da coluna é obrigatório!")
        .min(5, "O nome deve ter no mínimo 5 caracteres!"),
    }),
    onSubmit: async (values) => {
      setLoading(true);
      try {
        if (data) {
          const column = await editColumn({ id: data.id, name: values.name });

          client.setQueryData<ExpandedBoard>(["board", boardId], (prev) => {
            if (!prev) {
              throw new Error("Cache inválido");
            }

            const columnId = prev.columns.findIndex((x) => x.id === column.id);
            prev.columns[columnId].name = column.name;

            return prev;
          });
        } else if (boardId) {
          const column = await createColumn({
            name: values.name,
            boardId: boardId,
          });

          client.setQueryData<ExpandedBoard>(["board", boardId], (prev) => {
            if (!prev) {
              throw new Error("Cache inválido");
            }

            prev.columns.push(column);

            return prev;
          });
        }
        onSuccess();
      } catch (e) {
        console.log(e);
        setLoading(false);
      }
    },
  });

  return (
    <Form onSubmit={formik.handleSubmit}>
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
      <FilledButton loading={loading ? 1 : 0} type="submit">
        Salvar
      </FilledButton>
    </Form>
  );
}
