import { CreateItemProps, ExpandedItem, Item } from "@/types/item";
import { MessageResponse } from "@/types/responses/message";
import http from "../http";

export async function getItens() {
  const { data } = await http.get<ExpandedItem[]>("/itens");
  return data;
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
