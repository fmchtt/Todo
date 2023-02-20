import { Text } from "../../assets/css/global.styles";
import { ExpandedColumn } from "../../types/column";
import { Item } from "../../types/item";
import ItemCard from "../itemCard";
import { ColumnBody, ColumnHeading, ColumnStyled } from "./styles";

type ColumnProps = {
  data: ExpandedColumn;
  totalItems: number;
  onItemClick: (item: Item) => void;
};

export default function Column({ data, totalItems, onItemClick }: ColumnProps) {
  return (
    <ColumnStyled>
      <ColumnHeading>
        <Text>{data.name}</Text>
        <Text>
          {data.itemCount} / {totalItems}
        </Text>
      </ColumnHeading>
      <ColumnBody>
        {data.itens.map((item) => {
          return (
            <ItemCard
              key={item.id}
              onClick={() => onItemClick(item)}
              data={item}
            />
          );
        })}
      </ColumnBody>
    </ColumnStyled>
  );
}
