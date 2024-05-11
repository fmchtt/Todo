import { Item } from "./item";

export enum ColumnType {
  OPEN,
  PROGRESS,
  DONE,
}

export type CreateColumn = {
  name: string;
  boardId: string;
  type: number;
};

export type EditColumn = {
  id: string;
  boardId: string;
  name?: string;
  order?: number;
  type?: number;
};

export type DeleteColumn = {
  id: string;
  boardId: string;
};

export interface ExpandedColumn {
  id: string;
  name: string;
  order: number;
  itemCount: number;
  type: number;
  itens: Item[];
}
