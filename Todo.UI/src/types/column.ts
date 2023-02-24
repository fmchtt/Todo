import { Item } from "./item";

export interface CreateColumn {
  name: string;
  boardId: string;
}

export interface EditColumn {
  id: string;
  name?: string;
  order?: number;
}
export interface ExpandedColumn {
  id: string;
  name: string;
  order: number;
  itemCount: number;
  itens: Item[];
}
