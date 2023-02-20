import { ExpandedColumn } from "./column";

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
  itemCount: number;
  doneItemCount: number;
}
