import User from "./user";

export interface Item {
  id: string;
  title: string;
  description: string;
  priority: number;
  createdDate: string;
  updatedDate: string;
  creator: User;
  files: string;
  done: string;
}

export type CreateItemProps = {
  columnId?: string;
  boardId?: string;
  title: string;
  description: string;
  priority: number;
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
