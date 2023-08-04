import {
  CommentLine,
  CommentLineContainer,
  CommentSectionContainer,
  UserContainer,
  CommentHead,
  CommentControls,
} from "@/components/commentSection/styles";
import moment from "moment";
import { Comment } from "@/types/comment";
import { useQuery, useQueryClient } from "@tanstack/react-query";
import { useState } from "react";
import { toast } from "react-toastify";
import {
  createComment,
  deleteComment,
  getCommentsByItemId,
} from "@/services/api/comment";
import { Input, InputGroup, Label } from "@/components/forms/styles";
import FilledButton from "@/components/filledButton";
import { PageResult } from "@/types/responses/page";
import { Text } from "@/assets/css/global.styles";
import RoundedAvatar from "@/components/roundedAvatar";
import { TbTrash } from "react-icons/tb";

type CommentSectionProps = {
  itemId: string;
};
export default function CommentSection(props: CommentSectionProps) {
  const [text, setText] = useState("");
  const [loading, setLoading] = useState(false);

  const client = useQueryClient();
  const { data } = useQuery({
    queryKey: ["comments", props.itemId],
    queryFn: () => getCommentsByItemId(props.itemId),
  });

  async function handleCreateComment() {
    if (text == "") {
      return;
    }

    try {
      setLoading(true);
      const comment = await createComment(props.itemId, text);
      client.setQueryData<PageResult<Comment>>(
        ["comments", props.itemId],
        (prev) => {
          if (!prev) {
            return prev;
          }

          prev.results.unshift(comment);
          prev.pageCount += 1;

          return prev;
        }
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
        (prev) => {
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
        }
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
        <InputGroup row>
          <Input
            flexible
            value={text}
            onChange={(e) => {
              setText(e.target.value);
            }}
          />
          <FilledButton
            onClick={() => handleCreateComment()}
            loading={loading ? 1 : 0}
            margin="0"
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
                    size={40}
                    avatarUrl={
                      comment.author.avatarUrl &&
                      import.meta.env.VITE_API_URL + comment.author.avatarUrl
                    }
                    name={comment.author.name}
                  />
                  <div>
                    <Text>{comment.author.name}</Text>
                    <Text size="thin">
                      Atualizado em:{" "}
                      {moment(comment.updateTimeStamp).format(
                        "DD/MM/YYYY HH:mm"
                      )}
                    </Text>
                  </div>
                </UserContainer>
                <CommentControls>
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
