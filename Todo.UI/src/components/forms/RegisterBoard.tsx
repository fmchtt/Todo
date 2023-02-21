import { useState } from "react";
import { FormBoardProps } from "./types";
import { Form, Label, InputGroup, Input, TextArea } from "./styles";
import FilledButton from "../filledButton";
import { H1 } from "@/assets/css/global.styles";
import { useFormik } from "formik";
import { patchBoard, postBoard } from "@/services/api/boards";
import * as Yup from "yup";
import { useMutation, useQueryClient } from "react-query";

const BoardRegister = (props: FormBoardProps) => {
  const [loading, setLoading] = useState(false);
  const client = useQueryClient();
  const create = useMutation(postBoard, {
    onSuccess: () => {
      setLoading(true);
      client.invalidateQueries(["boards"]);
      setLoading(false);
      if (props.closeModal) {
        props.closeModal();
      }
    },
  });

  const update = useMutation(patchBoard, {
    onSuccess: () => {
      setLoading(true);
      client.invalidateQueries(["board", props.data?.id]);
      client.invalidateQueries(["boards"]);
      setLoading(false);
      if (props.closeModal) {
        props.closeModal();
      }
    },
  });

  const formik = useFormik({
    initialValues: {
      name: props.data?.name || "",
      description: props.data?.description || "",
    },
    onSubmit: (values) => {
      if (props.data?.id) {
        update.mutate({ id: props.data.id, values });
      } else {
        create.mutate(values);
      }
    },
    validationSchema: Yup.object({
      name: Yup.string().required("Este campo é obrigatório"),
    }),
    validateOnMount: false,
    validateOnBlur: false,
    validateOnChange: false,
  });
  return (
    <Form onSubmit={formik.handleSubmit} width="30vw">
      <H1>{props.data?.id ? "Editar" : "Criar"} Quadro</H1>
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
          name="description"
          value={formik.values.description}
          onChange={formik.handleChange}
        />
      </InputGroup>
      <FilledButton type="submit" loading={loading ? 1 : 0}>
        Cadastrar
      </FilledButton>
    </Form>
  );
};

export default BoardRegister;
