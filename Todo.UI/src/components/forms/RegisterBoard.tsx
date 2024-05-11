import { FormBoardProps } from "./types";
import * as Yup from "yup";
import { toast } from "react-toastify";
import Form from "../form";
import { useBoardCreate, useBoardUpdate } from "@/adapters/boardAdapters";

export default function BoardRegister(props: FormBoardProps) {
  const createMutation = useBoardCreate({
    onSuccess: () => {
      toast.success("Quadro criado com sucesso!");
      if (props.closeModal) {
        props.closeModal();
      }
    },
    onError: () => {
      toast.error("Erro ao criar quadro, tente novamente mais tarde!");
    },
  });

  const updateMutation = useBoardUpdate({
    onSuccess: () => {
      toast.success("Quadro atualizado com sucesso!");
      if (props.closeModal) {
        props.closeModal();
      }
    },
    onError: () => {
      toast.error("Erro ao atualizar quadro, tente novamente mais tarde!");
    },
  });

  const validationSchema = Yup.object({
    name: Yup.string()
      .required("O nome do Quadro é obrigatório!")
      .min(5, "O nome deve ter no mínimo 5 caracteres!"),
    description: Yup.string()
      .required("A descrição do Quadro é obrigatório!")
      .min(10, "A descrição deve ter no mínimo 10 caracteres!"),
  });

  return (
    <Form
      title={`${props.data?.id ? "Editar" : "Criar"} Quadro`}
      initialValues={{
        name: props.data?.name || "",
        description: props.data?.description || "",
      }}
      validationSchema={validationSchema}
      onSubmit={(values) => {
        if (props.data?.id) {
          updateMutation.mutate({ id: props.data.id, ...values });
        } else {
          createMutation.mutate(values);
        }
      }}
      width="30vw"
    >
      <Form.Input name="name" label="Nome" />
      <Form.TextArea name="description" label="Descrição" rows={8} />
      <Form.Submit
        label="Cadastrar"
        $loading={createMutation.isPending || updateMutation.isPending}
      />
    </Form>
  );
}
