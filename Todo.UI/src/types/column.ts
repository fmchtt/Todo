import { Item } from "./item";

export interface CreateColumn {
  name: string;
  boardId: string;
}

export interface EditColumn {
  id: string;
  name: string;
}
export interface ExpandedColumn {
  id: string;
  name: string;
  itemCount: number;
  itens: Item[];
}
