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
import { changeColumn } from "@/services/api/itens";

type ColumnProps = {
  data: ExpandedColumn;
  totalItems: number;
  onItemClick: (item: Item) => void;
};
let itemId = "";
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

  async function handleColumnChange() {
    if (itemId === "" || data.itens.filter((x) => x.id == itemId).length > 0) {
      return;
    }
    console.log(`${itemId} para ${data.id}`);
    await changeColumn(itemId, data.id);
    client.invalidateQueries(["board"]);
  }

  return (
    <ColumnStyled
      onDragOver={(e) => {
        e.preventDefault();
      }}
      onDrop={() => {
        handleColumnChange();
      }}
    >
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
              draggable
              onDragStart={(id) => {
                itemId = id;
              }}
            />
          );
        })}
      </ColumnBody>
    </ColumnStyled>
  );
}
