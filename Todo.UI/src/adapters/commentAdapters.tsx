import commentService from "@/services/commentService";
import { MutationAdapter } from "@/types/adapters";
import { Comment } from "@/types/comment";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { AxiosError } from "axios";
import { produce } from "immer";

export function useComments(itemId?: string) {
  return useQuery({
    queryKey: ["comments", itemId],
    queryFn: () => commentService.getCommentsByItemId(itemId || ""),
    enabled: !!itemId,
  });
}

type CommentCreateProps = MutationAdapter<Comment>;
export function useCommentCreate(props?: CommentCreateProps) {
  const client = useQueryClient();

  return useMutation({
    mutationFn: commentService.createComment,
    onSuccess(data, variables) {
      const queryCache = client.getQueryCache();

      const commentsQuery = queryCache.find({
        queryKey: ["comments", variables.itemId],
        exact: true,
      });
      if (commentsQuery) {
        client.setQueryData<Comment[]>(
          commentsQuery.queryKey,
          produce((draft) => {
            if (!draft) return;
            draft.push(data);
          })
        );
      }

      if (props?.onSuccess) props.onSuccess(data);
    },
    onError(error) {
      if (error instanceof AxiosError) {
        if (props?.onError) props.onError(error);
      } else {
        console.error(error);
      }
    },
  });
}

type CommentUpdateProps = MutationAdapter<Comment>;
export function useCommentUpdate(props?: CommentUpdateProps) {
  const client = useQueryClient();

  return useMutation({
    mutationFn: commentService.updateComment,
    onSuccess(data, variables) {
      const queryCache = client.getQueryCache();

      const commentQuery = queryCache.find({
        queryKey: ["comments", variables.itemId],
        exact: true,
      });
      if (commentQuery) {
        client.setQueryData<Comment[]>(
          commentQuery.queryKey,
          produce((draft) => {
            if (!draft) return;
            const commentIdx = draft.findIndex((x) => x.id === variables.id);
            if (commentIdx === -1) return;
            draft[commentIdx] = data;
          })
        );
      }

      if (props?.onSuccess) props.onSuccess(data);
    },
    onError(error) {
      if (error instanceof AxiosError) {
        if (props?.onError) props.onError(error);
      } else {
        console.error(error);
      }
    },
  });
}

type CommentDeleteProps = MutationAdapter<string>;
export function useCommentDelete(props?: CommentDeleteProps) {
  const client = useQueryClient();

  return useMutation({
    mutationFn: commentService.deleteComment,
    onSuccess(data, variables) {
      const queryCache = client.getQueryCache();

      const commentQuery = queryCache.find({
        queryKey: ["comments", variables.itemId],
        exact: true,
      });
      if (commentQuery) {
        client.setQueryData<Comment[]>(
          commentQuery.queryKey,
          produce((draft) => {
            if (!draft) return;
            const commentIdx = draft.findIndex((x) => x.id === variables.id);
            if (commentIdx === -1) return;
            draft.splice(commentIdx, 1);
          })
        );
      }

      if (props?.onSuccess) props.onSuccess(variables.id);
    },
    onError(error) {
      if (error instanceof AxiosError) {
        if (props?.onError) props.onError(error);
      } else {
        console.error(error);
      }
    },
  });
}
