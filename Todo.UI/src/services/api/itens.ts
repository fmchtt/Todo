import { CreateItemProps, ExpandedItem, Item } from "@/types/item";
import { MessageResponse } from "@/types/responses/message";
import http from "../http";
import { PageResult } from "@/types/responses/page";

export async function getItens() {
  const { data } = await http.get<PageResult<ExpandedItem[]>>("/itens");
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
