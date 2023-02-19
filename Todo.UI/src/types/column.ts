import { Item } from "./item";

export interface ExpandedColumn {
  id: string;
  name: string;
  itemCount: number;
  itens: Item[];
}
