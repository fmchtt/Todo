import {
  CommentLine,
  CommentLineContainer,
  CommentSectionContainer,
  UserContainer,
  CommentHead,
  CommentControls,
} from "@/components/commentSection/styles";
import { FormEvent } from "react";
import moment from "moment";
import { Comment } from "@/types/comment";
import { useQuery, useQueryClient } from "@tanstack/react-query";
import { useState } from "react";
import { toast } from "react-toastify";
import {
  createComment,
  deleteComment,
  editComment,
  getCommentsByItemId,
} from "@/services/api/comment";
import { Input, InputGroup, Label } from "@/components/forms/styles";
import FilledButton from "@/components/filledButton";
import { PageResult } from "@/types/responses/page";
import { Text } from "@/assets/css/global.styles";
import RoundedAvatar from "@/components/roundedAvatar";
import { TbTrash, TbEdit, TbCheck } from "react-icons/tb";
import useAuth from "@/context/auth";
import { produce } from "immer";

type CommentSectionProps = {
  itemId: string;
};
export default function CommentSection(props: CommentSectionProps) {
  const [text, setText] = useState("");
  const [commentEditId, setCommentEditId] = useState("");
  const [loading, setLoading] = useState(false);

  const { user } = useAuth();

  const client = useQueryClient();
  const { data } = useQuery({
    queryKey: ["comments", props.itemId],
    queryFn: () => getCommentsByItemId(props.itemId),
  });

  async function handleEditComment(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    const formData = new FormData(e.target as HTMLFormElement);
    const text = formData.get("text");
    if (!text || text == "") {
      return;
    }

    try {
      setLoading(true);
      const comment = await editComment(commentEditId, text.toString());
      client.setQueryData<PageResult<Comment>>(
        ["comments", props.itemId],
        produce((prev) => {
          if (!prev) {
            return prev;
          }

          const commentIdx = prev.results.findIndex(
            (x) => x.id === commentEditId,
          );
          if (commentIdx === -1) {
            return prev;
          }

          prev.results[commentIdx] = comment;

          return prev;
        }),
      );

      setText("");
      toast.success("Comentário editado!");
    } catch (e) {
      toast.error("Oops! Ocorreu um erro, tente novamente mais tarde!");
    } finally {
      setCommentEditId("");
      setLoading(false);
    }
  }

  async function handleCreateComment() {
    if (text == "") {
      return;
    }

    try {
      setLoading(true);
      const comment = await createComment(props.itemId, text);
      client.setQueryData<PageResult<Comment>>(
        ["comments", props.itemId],
        produce((prev) => {
          if (!prev) {
            return prev;
          }

          prev.results.unshift(comment);
          prev.pageCount += 1;

          return prev;
        }),
      );

      setText("");
      toast.success("Comentário adicionado!");
    } catch (e) {
      toast.error("Oops! Ocorreu um erro, tente novamente mais tarde!");
    } finally {
      setLoading(false);
    }
  }

  async function handleDeleteComment(id: string) {
    try {
      setLoading(true);
      await deleteComment(id);
      client.setQueryData<PageResult<Comment>>(
        ["comments", props.itemId],
        produce((prev) => {
          if (!prev) {
            return prev;
          }

          const commentIdx = prev.results.findIndex((x) => x.id === id);
          if (commentIdx === -1) {
            return prev;
          }

          prev.results.splice(commentIdx, 1);
          prev.pageCount -= 1;

          return prev;
        }),
      );

      setText("");
      toast.success("Comentário removido!");
    } catch (e) {
      toast.error("Oops! Ocorreu um erro, tente novamente mais tarde!");
    } finally {
      setLoading(false);
    }
  }

  return (
    <CommentSectionContainer>
      <InputGroup>
        <Label>Escrever comentário:</Label>
        <InputGroup $row>
          <Input
            $flexible
            value={text}
            onChange={(e) => {
              setText(e.target.value);
            }}
          />
          <FilledButton
            onClick={() => handleCreateComment()}
            $loading={loading}
            $margin="0"
            $height="unset"
          >
            Comentar
          </FilledButton>
        </InputGroup>
      </InputGroup>
      <CommentLineContainer>
        {data?.results.map((comment) => {
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
                        "DD/MM/YYYY HH:mm",
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
                      onClick={() => setCommentEditId(comment.id)}
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
              {commentEditId === comment.id ? (
                <form onSubmit={handleEditComment}>
                  <InputGroup $row>
                    <Input name="text" $flexible defaultValue={comment.text} />
                    <FilledButton
                      $loading={loading}
                      $margin="0"
                      $height="unset"
                      type="submit"
                    >
                      <TbCheck role="button" cursor="pointer" size={30} />
                    </FilledButton>
                  </InputGroup>
                </form>
              ) : (
                <Text>{comment.text}</Text>
              )}
            </CommentLine>
          );
        })}
      </CommentLineContainer>
    </CommentSectionContainer>
  );
}
