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
import { ExpandedBoard } from "@/types/board";

type ColumnProps = {
  data: ExpandedColumn;
  totalItems: number;
  boardId: string;
  onItemClick: (item: Item) => void;
};
let itemId = "";
export default function Column({
  data,
  totalItems,
  onItemClick,
  boardId,
}: ColumnProps) {
  const client = useQueryClient();

  const [handleColumnModal, columnModal] = useModal(
    <ColumnForm
      data={{ id: data.id, name: data.name }}
      boardId={boardId}
      onSuccess={handleColumnModalSuccess}
    />
  );

  function handleColumnModalSuccess() {
    handleColumnModal();
  }

  async function handleColumnChange() {
    if (itemId === "" || data.itens.filter((x) => x.id == itemId).length > 0) {
      return;
    }
    console.log(`${itemId} para ${data.id}`);
    await changeColumn(itemId, data.id);

    client.setQueryData<ExpandedBoard>(["board", boardId], (prev) => {
      if (prev == undefined) {
        throw new Error("Cache invalido");
      }

      const oldColIdx = prev.columns.findIndex(
        (x) => x.itens.findIndex((x) => x.id === itemId) !== -1
      );
      prev.columns[oldColIdx].itemCount -= 1;

      const itemIdx = prev.columns[oldColIdx].itens.findIndex(
        (x) => x.id === itemId
      );
      const item = prev.columns[oldColIdx].itens.splice(itemIdx, 1);

      const newColIdx = prev.columns.findIndex((x) => x.id === data.id);
      prev.columns[newColIdx].itens.push(item[0]);
      prev.columns[newColIdx].itemCount += 1;

      return prev;
    });
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
