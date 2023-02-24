import { ExpandedColumn } from "./column";
import User from "./user";

export interface EditBoard {
  id: string;
  name?: string;
  description?: string;
}

export interface ResumedBoard {
  id: string;
  name: string;
  description: string;
  itemCount: number;
  doneItemCount: number;
}

export interface ExpandedBoard {
  id: string;
  name: string;
  description: string;
  columns: ExpandedColumn[];
  participants: User[];
  itemCount: number;
  doneItemCount: number;
}
