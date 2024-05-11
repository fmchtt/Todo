import itemService from "@/services/itemService";
import { MutationAdapter } from "@/types/adapters";
import { ExpandedBoard } from "@/types/board";
import { ExpandedItem, Item } from "@/types/item";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { AxiosError } from "axios";
import { produce } from "immer";

export function useItems(boardId?: string) {
  return useQuery({
    queryKey: boardId ? ["itens", "board", boardId] : ["itens"],
    queryFn: () => itemService.getItens({ boardId }),
  });
}

export function useItem(id: string) {
  return useQuery({
    queryKey: ["item", id],
    queryFn: () => itemService.getItens({ id }),
  });
}

type ItemCreateProps = MutationAdapter<Item>;
export function useItemCreate(props?: ItemCreateProps) {
  const client = useQueryClient();

  return useMutation({
    mutationFn: itemService.createItem,
    onSuccess(data, variables) {
      const queryCache = client.getQueryCache();

      const itensQuery = queryCache.find({ queryKey: ["itens"], exact: true });
      if (itensQuery) {
        client.setQueryData<ExpandedItem[]>(
          itensQuery.queryKey,
          produce((draft) => {
            if (!draft) return;
            draft.push(data);
          })
        );
      }

      if (variables.boardId && variables.columnId) {
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
                (x) => x.id === variables.columnId
              );
              if (columnIdx === -1) return;
              draft.columns[columnIdx].itens.push(data);
            })
          );
        }
      }

      client.setQueryData<ExpandedItem>(["item", data.id], data);

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

type ItemUpdateProps = MutationAdapter<Item>;
export function useItemUpdate(props?: ItemUpdateProps) {
  const client = useQueryClient();

  return useMutation({
    mutationFn: itemService.updateItem,
    onSuccess(data) {
      const queryCache = client.getQueryCache();

      const itensQuery = queryCache.find({ queryKey: ["itens"], exact: true });
      if (itensQuery) {
        client.setQueryData<ExpandedItem[]>(
          itensQuery.queryKey,
          produce((draft) => {
            if (!draft) return;
            const itemIdx = draft.findIndex((x) => x.id === data.id);
            if (itemIdx === -1) return;
            draft[itemIdx] = data;
          })
        );
      }

      client.setQueryData<ExpandedItem>(["item", data.id], data);

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

type ItemDeleteProps = MutationAdapter<string>;
export function useItemDelete(props?: ItemDeleteProps) {
  const client = useQueryClient();

  return useMutation({
    mutationFn: itemService.deleteItem,
    onSuccess(data, id) {
      const queryCache = client.getQueryCache();

      const itensQuery = queryCache.find({ queryKey: ["itens"], exact: true });
      if (itensQuery) {
        client.setQueryData<ExpandedItem[]>(
          itensQuery.queryKey,
          produce((draft) => {
            if (!draft) return;
            const itemIdx = draft.findIndex((x) => x.id === id);
            if (itemIdx === -1) return;
            draft.splice(itemIdx, 1);
          })
        );
      }

      client.removeQueries({
        queryKey: ["item", id],
        exact: true,
      });

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

type ItemDoneProps = MutationAdapter<Item>;
export function useItemDone(props?: ItemDoneProps) {
  const client = useQueryClient();

  return useMutation({
    mutationFn: itemService.toggleItemDone,
    onSuccess(data) {
      const queryCache = client.getQueryCache();

      const itensQuery = queryCache.find({ queryKey: ["itens"], exact: true });
      if (itensQuery) {
        client.setQueryData<ExpandedItem[]>(
          itensQuery.queryKey,
          produce((draft) => {
            if (!draft) return;
            const itemIdx = draft.findIndex((x) => x.id === data.id);
            if (itemIdx === -1) return;
            draft[itemIdx].done = data.done;
          })
        );
      }

      const itensByIdQuery = queryCache.find({
        queryKey: ["item", data.id],
        exact: true,
      });
      if (itensByIdQuery) {
        client.setQueryData<Item>(
          itensByIdQuery.queryKey,
          produce((draft) => {
            if (!draft) return;
            draft.done = data.done;
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

type ItemChangeColumnProps = MutationAdapter<Item>;
export function useItemColumnChange(props?: ItemChangeColumnProps) {
  const client = useQueryClient();

  return useMutation({
    mutationFn: itemService.changeItemColumn,
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

            const originColumnIdx = draft.columns.findIndex(
              (x) => x.itens.findIndex((x) => x.id === variables.itemId) !== -1
            );
            if (originColumnIdx === -1) return;
            draft.columns[originColumnIdx].itemCount -= 1;

            const itemIdx = draft.columns[originColumnIdx].itens.findIndex(
              (x) => x.id === variables.itemId
            );
            if (itemIdx === -1) return;
            const item = draft.columns[originColumnIdx].itens.splice(
              itemIdx,
              1
            );

            if (variables.targetColumnType === 2) {
              item[0].done = true;
            } else {
              item[0].done = false;
            }

            const targetColumnIdx = draft.columns.findIndex(
              (x) => x.id === variables.targetColumnId
            );
            if (targetColumnIdx === -1) return;

            draft.columns[targetColumnIdx].itens.push(item[0]);
            draft.columns[targetColumnIdx].itemCount += 1;
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
