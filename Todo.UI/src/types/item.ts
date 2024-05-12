import { User } from "./user";
import { ResumedBoard } from "./board";
import { ResumedColumn } from "./column";

export type Item = {
  id: string;
  title: string;
  description: string;
  priority: number;
  createdDate: string;
  updatedDate: string;
  creator: User;
  files: string;
  done: boolean;
};

export type ExpandedItem = Item & {
  board?: ResumedBoard;
  column?: ResumedColumn;
};

export type QueryItens = {
  boardId?: string;
  id?: string;
};

export type CreateItem = {
  columnId?: string;
  boardId?: string;
  title: string;
  description: string;
  priority: number;
};

export type EditItem = {
  id: string;
  title?: string;
  description?: string;
  priority?: number;
  columnId?: string;
  boardId?: string;
};

export type ToggleDone = {
  id: string;
  done: boolean;
};

export type ChangeColumn = {
  itemId: string;
  boardId: string;
  originColumnId: string;
  targetColumnId: string;
  targetColumnType: number;
};

export function getPriorityDisplay(priority: number) {
  switch (priority) {
    case 0:
      return "Nenhuma";
    case 1:
      return "Muito Baixa";
    case 2:
      return "Baixa";
    case 3:
      return "Media";
    case 4:
      return "Alta";
    case 5:
      return "Muita Alta";
    default:
      return "Nenhuma";
  }
}
