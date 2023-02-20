import { Item } from "../../types/item";
import http from "../http";

export async function getItens() {
  const { data } = await http.get<Item[]>("/itens");
  return data;
}

type CreateItemProps = {
  columnId?: string;
  boardId?: string;
  title: string;
  description: string;
  priority: number;
};
export async function createItem(reqData: CreateItemProps) {
  const { data } = await http.post<Item>("/itens", reqData);
  return data;
}

export async function deleteItem(id: string) {
  const { data } = await http.delete<MessageResponse>(`/itens/${id}`);
  return data;
}
