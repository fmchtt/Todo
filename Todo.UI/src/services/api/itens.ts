import { CreateItemProps, ExpandedItem, Item } from "@/types/item";
import { MessageResponse } from "@/types/responses/message";
import http from "../http";
import { PageResult } from "@/types/responses/page";
import { QueryFunctionContext } from "@tanstack/react-query";

export async function getItens(context: QueryFunctionContext<string[]>) {
  let url = "/itens";

  if (context.queryKey.length > 1) {
    url = url + "?boardId=" + context.queryKey[1];
  }

  const { data } = await http.get<PageResult<ExpandedItem>>(url);
  return data.results;
}
export async function createItem(reqData: CreateItemProps) {
  const { data } = await http.post<Item>("/itens", reqData);
  return data;
}

export async function deleteItem(id: string) {
  const { data } = await http.delete<MessageResponse>(`/itens/${id}`);
  return data;
}

export async function changeColumn(todoId: string, columnId: string) {
  const { data } = await http.post<Item>(`/itens/${todoId}/column/${columnId}`);
  return data;
}

export async function changeDone(id: string, done: boolean) {
  const { data } = await http.post<Item>(
    `/itens/${id}/${done ? "done" : "undone"}`
  );
  return data;
}
