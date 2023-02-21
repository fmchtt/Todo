import { useState } from "react";
import { useFormik } from "formik";
import FilledButton from "../filledButton";
import { Form, Input, InputGroup, Label } from "./styles";
import { EditColumn } from "@/types/column";
import { createColumn, editColumn } from "@/services/api/column";

type ColumnFormProps = {
  data?: EditColumn;
  boardId?: string;
  onSuccess: () => void;
};
export default function ColumnForm({
  data,
  onSuccess,
  boardId,
}: ColumnFormProps) {
  const [loading, setLoading] = useState(false);

  const formik = useFormik({
    initialValues: {
      name: data?.name || "",
    },
    onSubmit: async (values) => {
      setLoading(true);
      try {
        if (data) {
          await editColumn({ id: data.id, name: values.name });
        } else if (boardId) {
          await createColumn({ name: values.name, boardId: boardId });
        }
        onSuccess();
      } catch (e) {
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
      </InputGroup>
      <FilledButton loading={loading ? 1 : 0} type="submit">
        Salvar
      </FilledButton>
    </Form>
  );
}
