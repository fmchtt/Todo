import { FormBoardProps } from "./types";
import { Form, Label, InputGroup, Input, TextArea } from "./styles";
import FilledButton from "../filledButton";
import { H1 } from "@/assets/css/global.styles";
import { useFormik } from "formik";
import { patchBoard, postBoard } from "@/services/api/boards";
import * as Yup from "yup";
import { useMutation, useQueryClient } from "react-query";
import { ExpandedBoard, ResumedBoard } from "@/types/board";
import ErrorMessage from "@/components/forms/ErrorMessage";

const BoardRegister = (props: FormBoardProps) => {
  const client = useQueryClient();
  const createMutation = useMutation(postBoard, {
    onSuccess: (res) => {
      client.setQueryData<ResumedBoard[]>(["boards"], (prev) => {
        if (!prev) {
          throw new Error("Cache invalido!");
        }

        prev.push(res);

        return prev;
      });

      if (props.closeModal) {
        props.closeModal();
      }
    },
  });

  const updateMutation = useMutation(patchBoard, {
    onSuccess: (res) => {
      try {
        client.setQueryData<ResumedBoard[]>(["boards"], (prev) => {
          if (!prev) {
            throw new Error("Cache invalido!");
          }

          const boardIdx = prev.findIndex((x) => x.id === props.data?.id);
          if (boardIdx >= 0) {
            prev[boardIdx].name = res.name;
            prev[boardIdx].description = res.description;
          }

          return prev;
        });
      } catch (e) {
        console.log();
      }

      client.setQueryData<ExpandedBoard>(["board", props.data?.id], (prev) => {
        if (!prev) {
          throw new Error("Cache invalido!");
        }

        prev.name = res.name;
        prev.description = res.description;

        return prev;
      });

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
        updateMutation.mutate({ id: props.data.id, values });
      } else {
        createMutation.mutate(values);
      }
    },
    validationSchema: Yup.object({
      name: Yup.string()
        .required("O nome do Quadro ?? obrigat??rio!")
        .min(5, "O nome deve ter no m??nimo 5 caracteres!"),
      description: Yup.string()
        .required("A descri????o do Quadro ?? obrigat??rio!")
        .min(10, "A descri????o deve ter no m??nimo 10 caracteres!"),
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
        {formik.errors.name && (
          <ErrorMessage>{formik.errors.name}</ErrorMessage>
        )}
      </InputGroup>
      <InputGroup>
        <Label>Descri????o</Label>
        <TextArea
          rows={8}
          name="description"
          value={formik.values.description}
          onChange={formik.handleChange}
        />
        {formik.errors.description && (
          <ErrorMessage>{formik.errors.description}</ErrorMessage>
        )}
      </InputGroup>
      <FilledButton
        type="submit"
        loading={createMutation.isLoading || updateMutation.isLoading ? 1 : 0}
      >
        Cadastrar
      </FilledButton>
    </Form>
  );
};

export default BoardRegister;
