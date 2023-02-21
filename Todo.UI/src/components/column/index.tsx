import { Text } from "@/assets/css/global.styles";
import { ExpandedColumn } from "@/types/column";
import { Item } from "@/types/item";
import ItemCard from "../itemCard";
import {
  ColumnActions,
  ColumnBody,
  ColumnHeading,
  ColumnStyled,
} from "./styles";
import { TbEdit } from "react-icons/tb";
import { useModal } from "@/hooks";
import ColumnForm from "../forms/ColumnForm";
import { useQueryClient } from "react-query";

type ColumnProps = {
  data: ExpandedColumn;
  totalItems: number;
  onItemClick: (item: Item) => void;
};

export default function Column({ data, totalItems, onItemClick }: ColumnProps) {
  const client = useQueryClient();

  const [handleColumnModal, columnModal] = useModal(
    <ColumnForm
      data={{ id: data.id, name: data.name }}
      onSuccess={handleColumnModalSuccess}
    />
  );

  function handleColumnModalSuccess() {
    handleColumnModal();
    client.invalidateQueries("board");
  }

  return (
    <ColumnStyled>
      {columnModal}
      <ColumnHeading>
        <Text>{data.name}</Text>
        <ColumnActions>
          <TbEdit
            role="button"
            size={24}
            cursor="pointer"
            onClick={handleColumnModal}
          />
          <Text>
            {data.itemCount} / {totalItems}
          </Text>
        </ColumnActions>
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
