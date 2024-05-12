import columnService from "@/services/columnService";
import { MutationAdapter } from "@/types/adapters";
import { ExpandedBoard } from "@/types/board";
import { ExpandedColumn } from "@/types/column";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { AxiosError } from "axios";
import { produce } from "immer";

type ColumnCreateProps = MutationAdapter<ExpandedColumn>;
export function useColumnCreate(props?: ColumnCreateProps) {
  const client = useQueryClient();

  return useMutation({
    mutationFn: columnService.createColumn,
    onSuccess(data, variables) {
      const queryCache = client.getQueryCache();
      const value = { itemCount: 0, itens: [], ...data };

      const boardQuery = queryCache.find({
        queryKey: ["board", variables.boardId],
        exact: true,
      });
      if (boardQuery) {
        client.setQueryData<ExpandedBoard>(
          boardQuery.queryKey,
          produce((draft) => {
            if (!draft) return;
            draft.columns.push(value);
          })
        );
      }

      if (props?.onSuccess) props.onSuccess(value);
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

type ColumnUpdateProps = MutationAdapter<ExpandedColumn>;
export function useColumnUpdate(props?: ColumnUpdateProps) {
  const client = useQueryClient();

  return useMutation({
    mutationFn: columnService.updateColumn,
    onSuccess(data, variables) {
      const queryCache = client.getQueryCache();

      if (variables.order) {
        client.invalidateQueries({
          queryKey: ["board", variables.boardId],
          exact: true,
        });
      } else {
        const boardQuery = queryCache.find({
          queryKey: ["board", variables.boardId],
          exact: true,
        });
        if (boardQuery) {
          client.setQueryData<ExpandedBoard>(
            boardQuery.queryKey,
            produce((draft) => {
              if (!draft) return;
              const columnIdx = draft.columns.findIndex(
                (x) => x.id === data.id
              );
              if (columnIdx === -1) return;
              Object.assign(draft.columns[columnIdx], data);
            })
          );
        }
      }

      if (props?.onSuccess)
        props.onSuccess({ itemCount: 0, itens: [], ...data });
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

type ColumnDeleteProps = MutationAdapter<string>;
export function useColumnDelete(props?: ColumnDeleteProps) {
  const client = useQueryClient();

  return useMutation({
    mutationFn: columnService.deleteColumn,
    onSuccess(data, variables) {
      const queryCache = client.getQueryCache();

      const boardQuery = queryCache.find({
        queryKey: ["board", variables.boardId],
        exact: true,
      });
      if (boardQuery) {
        client.setQueryData<ExpandedBoard>(
          boardQuery.queryKey,
          produce((draft) => {
            if (!draft) return;
            const columnIdx = draft.columns.findIndex(
              (x) => x.id === variables.id
            );
            if (columnIdx === -1) return;
            draft.columns.splice(columnIdx, 1);
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
