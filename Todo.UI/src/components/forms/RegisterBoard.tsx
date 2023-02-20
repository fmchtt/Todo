import { useState } from "react";
import { FormBoardProps } from "./types";
import { Form, Label, InputGroup, Input, TextArea } from "./styles";
import FilledButton from "../filledButton";
import { H1 } from "../../assets/css/global.styles";
import { useFormik } from "formik";
import { postBoard } from "../../services/api/boards";
import * as Yup from "yup";
import { useMutation, useQueryClient } from "react-query";

const BoardRegister = (props: FormBoardProps) => {
  const [loading, setLoading] = useState(false);
  const client = useQueryClient();
  const mutation = useMutation(postBoard, {
    onSuccess: () => {
      setLoading(true);
      client.invalidateQueries(["boards"]);
      setLoading(false);
      if (props.closeModal) {
        props.closeModal();
      }
    },
  });

  const formik = useFormik({
    initialValues: {
      name: "",
      description: "",
    },
    onSubmit: (values) => {
      mutation.mutate(values);
    },
    validationSchema: Yup.object({
      name: Yup.string().required("Este campo é obrigatório"),
    }),
    validateOnMount: false,
    validateOnBlur: false,
    validateOnChange: false,
  });
  return (
    <Form {...props} onSubmit={formik.handleSubmit} width="30vw">
      <H1>Cadastrar Quadro</H1>
      <InputGroup>
        <Label>Nome</Label>
        <Input
          placeholder="Ex: Quadro 1"
          type="text"
          name="name"
          value={formik.values.name}
          onChange={formik.handleChange}
        />
      </InputGroup>
      <InputGroup>
        <Label>Descrição</Label>
        <TextArea
          rows={8}
          value={formik.values.description}
          onChange={formik.handleChange}
        />
      </InputGroup>
      <FilledButton loading={loading}>Cadastrar</FilledButton>
    </Form>
  );
};

export default BoardRegister;
