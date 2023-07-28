import { Item } from "./item";

export enum ColumnType {
  OPEN,
  PROGRESS,
  DONE,
}

export interface CreateColumn {
  name: string;
  boardId: string;
  type: number;
}

export interface EditColumn {
  id: string;
  name?: string;
  order?: number;
  type?: number;
}
export interface ExpandedColumn {
  id: string;
  name: string;
  order: number;
  itemCount: number;
  type: number;
  itens: Item[];
}
