import { ExpandedColumn } from "./column";

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
  columns: ExpandedColumn[];
  itemCount: number;
  doneItemCount: number;
}
