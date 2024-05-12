import { User } from "./user";
import { ResumedBoard } from "./board";
import { ResumedColumn } from "./column";
import {
  TbChevronDown,
  TbChevronsDown,
  TbChevronsUp,
  TbChevronUp,
  TbEqual,
  TbEqualNot,
} from "react-icons/tb";
import { IconType } from "react-icons";

export type ResumedItem = {
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

export type ExpandedItem = ResumedItem & {
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

export const priorityValues: { label: string; icon: IconType }[] = [
  { label: "Nenhuma", icon: TbEqualNot },
  { label: "Muito Baixa", icon: TbChevronsDown },
  { label: "Baixa", icon: TbChevronDown },
  { label: "Media", icon: TbEqual },
  { label: "Alta", icon: TbChevronUp },
  { label: "Muita Alta", icon: TbChevronsUp },
];

export function getPriorityDisplay(priority: number) {
  if (priority >= priorityValues.length) return priorityValues[0];
  return priorityValues[priority];
}
