import { Item } from "../../types/item";
import http from "../http";

export async function getItens() {
  const { data } = await http.get<Item[]>("/itens");
  return data;
}
