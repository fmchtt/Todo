import {
  ChangeColumn,
  CreateItem,
  EditItem,
  ExpandedItem,
  Item,
  QueryItens,
  ToggleDone,
} from "@/types/item";
import { PageResult } from "@/types/responses/page";
import http from "./http";
import { MessageResponse } from "@/types/responses/message";

class ItemService {
  async getItens(props: QueryItens) {
    let url = "/itens";

    if (props.boardId) {
      url += "?boardId=" + props.boardId;
    }

    if (props.id) {
      url += props.id + "/";
    }

    const { data } = await http.get<PageResult<ExpandedItem>>(url);
    return data.results;
  }

  async createItem(values: CreateItem) {
    const { data } = await http.post<Item>("/itens", values);
    return data;
  }

  async updateItem({ id, ...values }: EditItem) {
    const { data } = await http.patch<Item>(`/itens/${id}`, values);
    return data;
  }

  async deleteItem(id: string) {
    const { data } = await http.delete<MessageResponse>(`/itens/${id}`);
    return data;
  }

  async toggleItemDone({ id, done }: ToggleDone) {
    const { data } = await http.post<Item>(
      `/itens/${id}/${done ? "done" : "undone"}`
    );
    return data;
  }

  async changeItemColumn({ itemId, targetColumnId }: ChangeColumn) {
    const { data } = await http.post<Item>(
      `/itens/${itemId}/column/${targetColumnId}`
    );
    return data;
  }
}

export default new ItemService();
