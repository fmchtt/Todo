import { FormBoardProps } from "./types";
import { patchBoard, postBoard } from "@/services/api/boards";
import * as Yup from "yup";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { ExpandedBoard, ResumedBoard } from "@/types/board";
import { toast } from "react-toastify";
import { produce } from "immer";
import Form from "../form";

export default function BoardRegister(props: FormBoardProps) {
  const client = useQueryClient();
  const createMutation = useMutation({
    mutationFn: postBoard,
    onSuccess: (res) => {
      toast.success("Quadro criado com sucesso!");

      client.setQueryData<ResumedBoard[]>(
        ["boards"],
        produce((prev) => {
          if (!prev) {
            throw new Error("Cache invalido!");
          }

          prev.push(res);

          return prev;
        })
      );

      if (props.closeModal) {
        props.closeModal();
      }
    },
  });

  const updateMutation = useMutation({
    mutationFn: patchBoard,
    onSuccess: (res) => {
      toast.success("Quadro atualizado com sucesso!");

      if (client.getQueryCache().find({ queryKey: ["boards"] })) {
        client.setQueryData<ResumedBoard[]>(
          ["boards"],
          produce((prev) => {
            if (!prev) {
              return;
            }

            const boardIdx = prev.findIndex((x) => x.id === props.data?.id);
            if (boardIdx >= 0) {
              prev[boardIdx].name = res.name;
              prev[boardIdx].description = res.description;
            }
          })
        );
      }

      client.setQueryData<ExpandedBoard>(
        ["board", props.data?.id],
        produce((prev) => {
          if (!prev) {
            return;
          }

          prev.name = res.name;
          prev.description = res.description;
        })
      );

      if (props.closeModal) {
        props.closeModal();
      }
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
          updateMutation.mutate({ id: props.data.id, values });
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
