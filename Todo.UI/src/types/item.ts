import User from "./user";

export interface Item {
  id: string;
  title: string;
  description: string;
  createdDate: string;
  updatedDate: string;
  creator: User;
  files: string;
  done: string;
}
