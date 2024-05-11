import {
  CommentLine,
  CommentLineContainer,
  CommentSectionContainer,
  UserContainer,
  CommentHead,
  CommentControls,
} from "@/components/commentSection/styles";
import moment from "moment";
import { useRef } from "react";
import { toast } from "react-toastify";
import { InputGroup } from "@/components/forms/styles";
import { Text } from "@/assets/css/global.styles";
import RoundedAvatar from "@/components/roundedAvatar";
import { TbTrash, TbEdit } from "react-icons/tb";
import useAuth from "@/context/auth";
import {
  useCommentCreate,
  useCommentDelete,
  useComments,
  useCommentUpdate,
} from "@/adapters/commentAdapters";
import Form from "../form";
import * as Yup from "yup";
import { FormikProps } from "formik";

type CommentSectionProps = {
  itemId: string;
};
export default function CommentSection(props: CommentSectionProps) {
  const { user } = useAuth();
  const { data } = useComments(props.itemId);
  const formRef = useRef<FormikProps<{
    text: string;
    id: string;
  }> | null>();

  const createCommentMutation = useCommentCreate({
    onSuccess: () => {
      toast.success("Comentário adicionado!");
      formRef.current?.resetForm({
        values: {
          text: "",
          id: "",
        },
      });
    },
    onError: () => {
      toast.error(
        "Oops! Ocorreu um erro ao adicionar comentario, tente novamente mais tarde!"
      );
    },
  });

  const editCommentMutation = useCommentUpdate({
    onSuccess: () => {
      toast.success("Comentário editado!");
      formRef.current?.resetForm({
        values: {
          text: "",
          id: "",
        },
      });
    },
    onError: () => {
      toast.error(
        "Oops! Ocorreu um erro ao editar comentario, tente novamente mais tarde!"
      );
    },
  });

  const deleteCommentMutation = useCommentDelete({
    onSuccess: () => {
      toast.success("Comentário removido!");
    },
    onError: () => {
      toast.error(
        "Oops! Ocorreu um erro ao remover comentario, tente novamente mais tarde!"
      );
    },
  });

  async function handleDeleteComment(id: string) {
    deleteCommentMutation.mutate({
      id: id,
      itemId: props.itemId,
    });
  }

  function handleSubmit(values: { text: string; id: string | undefined }) {
    if (values.id) {
      editCommentMutation.mutate({
        id: values.id,
        text: values.text,
        itemId: props.itemId,
      });
    } else {
      createCommentMutation.mutate({
        itemId: props.itemId,
        text: values.text,
      });
    }
  }

  const validationSchema = Yup.object().shape({
    text: Yup.string().required("O texto é obrigatório!"),
  });

  return (
    <CommentSectionContainer>
      <Form
        initialValues={{ text: "", id: "" }}
        onSubmit={handleSubmit}
        validationSchema={validationSchema}
        innerRef={(ref) => (formRef.current = ref)}
      >
        <InputGroup $row>
          <Form.Input label="Escreva seu comentário:" name="text" />
          <Form.Submit
            label="Comentar"
            $loading={
              editCommentMutation.isPending ||
              createCommentMutation.isPending ||
              deleteCommentMutation.isPending
            }
          />
        </InputGroup>
      </Form>
      <CommentLineContainer>
        {data?.reverse().map((comment) => {
          return (
            <CommentLine key={comment.id}>
              <CommentHead>
                <UserContainer>
                  <RoundedAvatar
                    $size={40}
                    avatarUrl={
                      comment.author.avatarUrl &&
                      import.meta.env.VITE_API_URL + comment.author.avatarUrl
                    }
                    name={comment.author.name}
                  />
                  <div>
                    <Text>{comment.author.name}</Text>
                    <Text $size="thin">
                      Atualizado em:{" "}
                      {moment(comment.updateTimeStamp).format(
                        "DD/MM/YYYY HH:mm"
                      )}
                    </Text>
                  </div>
                </UserContainer>
                <CommentControls>
                  {comment.author.id === user?.id && (
                    <TbEdit
                      role="button"
                      size={20}
                      cursor="pointer"
                      onClick={() => {
                        formRef.current?.setFieldValue("id", comment.id);
                        formRef.current?.setFieldValue("text", comment.text);
                      }}
                    />
                  )}
                  <TbTrash
                    role="button"
                    size={20}
                    cursor="pointer"
                    onClick={() => handleDeleteComment(comment.id)}
                  />
                </CommentControls>
              </CommentHead>
              <Text>{comment.text}</Text>
            </CommentLine>
          );
        })}
      </CommentLineContainer>
    </CommentSectionContainer>
  );
}
