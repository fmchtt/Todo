import useAuth from "@/context/auth";
import boardService from "@/services/boardService";
import { MutationAdapter } from "@/types/adapters";
import { ExpandedBoard, ResumedBoard } from "@/types/board";
import { MessageResponse } from "@/types/responses/message";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { AxiosError } from "axios";
import { produce } from "immer";
import { useNavigate } from "react-router-dom";

export function useBoards() {
  return useQuery({
    queryKey: ["boards"],
    queryFn: boardService.getBoards,
  });
}

export function useBoard(id?: string) {
  return useQuery({
    queryKey: ["board", id],
    queryFn: () => boardService.getBoardById(id || ""),
    enabled: !!id,
  });
}

type BoardCreateProps = MutationAdapter<ResumedBoard>;
export function useBoardCreate(props?: BoardCreateProps) {
  const client = useQueryClient();

  return useMutation({
    mutationFn: boardService.createBoard,
    onSuccess(data) {
      const queryCache = client.getQueryCache();

      const boardsQuery = queryCache.find({
        queryKey: ["boards"],
        exact: true,
      });
      if (boardsQuery) {
        client.setQueryData<ResumedBoard[]>(
          boardsQuery.queryKey,
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

type BoardUpdateProps = MutationAdapter<ResumedBoard>;
export function useBoardUpdate(props?: BoardUpdateProps) {
  const client = useQueryClient();

  return useMutation({
    mutationFn: boardService.updateBoard,
    onSuccess(data) {
      const queryCache = client.getQueryCache();

      const boardQuery = queryCache.find({
        queryKey: ["board", data.id],
        exact: true,
      });
      if (boardQuery) {
        client.setQueryData<ExpandedBoard>(
          boardQuery.queryKey,
          produce((draft) => {
            if (!draft) return;
            draft.name = data.name;
            draft.description = data.description;
          })
        );
      }

      const boardsQuery = queryCache.find({
        queryKey: ["boards"],
        exact: true,
      });
      if (boardsQuery) {
        client.setQueryData<ResumedBoard[]>(
          boardsQuery.queryKey,
          produce((draft) => {
            if (!draft) return;

            const boardIdx = draft.findIndex((x) => x.id === data.id);
            if (boardIdx === -1) {
              draft.push(data);
              return;
            }

            draft[boardIdx].name = data.name;
            draft[boardIdx].description = data.description;
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

type BoardDeleteProps = MutationAdapter<string>;
export function useBoardDelete(props?: BoardDeleteProps) {
  const client = useQueryClient();

  return useMutation({
    mutationFn: boardService.deleteBoard,
    onSuccess(data, id) {
      const queryCache = client.getQueryCache();

      const boardsQuery = queryCache.find({
        queryKey: ["boards"],
        exact: true,
      });
      if (boardsQuery) {
        client.setQueryData<ResumedBoard[]>(
          boardsQuery.queryKey,
          produce((draft) => {
            if (!draft) return;
            const boardIdx = draft.findIndex((x) => x.id === id);
            if (boardIdx === -1) return;
            draft.splice(boardIdx, 1);
          })
        );
      }
      client.removeQueries({ queryKey: ["board", id], exact: true });

      if (props?.onSuccess) props.onSuccess(id);
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

type BoardParticipantAddProps = MutationAdapter<MessageResponse>;
export function useBoardParticipantAdd(props?: BoardParticipantAddProps) {
  return useMutation({
    mutationFn: boardService.sendBoardInvite,
    onSuccess(data) {
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

type BoardParticipantRemoveProps = MutationAdapter<MessageResponse>;
export function useBoardParticipantRemove(props?: BoardParticipantRemoveProps) {
  const client = useQueryClient();
  const { user } = useAuth();
  const navigate = useNavigate();

  return useMutation({
    mutationFn: boardService.deleteBoardParticipant,
    onSuccess(data, variables) {
      if (variables.participantId === user?.id) {
        client.removeQueries({
          queryKey: ["board"],
        });
        client.invalidateQueries({
          queryKey: ["boards"],
          exact: true,
        });
        client.invalidateQueries({
          queryKey: ["itens"],
          exact: true,
        });
        navigate("/home");
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

type BoardParticipantConfirmProps = MutationAdapter<MessageResponse>;
export function useBoardParticipantConfirm(
  props?: BoardParticipantConfirmProps
) {
  return useMutation({
    mutationFn: boardService.createBoardParticipant,
    onSuccess(data) {
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
