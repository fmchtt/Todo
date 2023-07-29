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
import { TbEdit, TbTrash } from "react-icons/tb";
import { useModal } from "@/hooks";
import ColumnForm from "../forms/ColumnForm";
import { useQueryClient } from "@tanstack/react-query";
import { changeColumn } from "@/services/api/itens";
import { ExpandedBoard } from "@/types/board";
import useConfirmationModal from "@/hooks/useConfirmationModal";
import { deleteColumn } from "@/services/api/column";

type ColumnProps = {
  data: ExpandedColumn;
  totalItems: number;
  boardId: string;
  onDragStart: (columnId: string) => void;
  onDragOver: (columnId: string) => void;
  onDragEnd: () => void;
  onItemClick: (item: Item) => void;
};
let itemId = "";
export default function Column({
  data,
  totalItems,
  onItemClick,
  boardId,
  onDragStart,
  onDragOver,
  onDragEnd,
}: ColumnProps) {
  const client = useQueryClient();

  const [columnModal, openColumnModal, closeColumnModal] = useModal(
    <ColumnForm
      data={{ id: data.id, name: data.name, type: data.type.toString() }}
      boardId={boardId}
      onSuccess={handleColumnModalSuccess}
    />
  );
  const [confirmationModal, openConfirmationModal] = useConfirmationModal({
    message: `Tem certeza que deseja apagar a coluna: ${data.name} ?`,
    onConfirm: handleConfirmationModalSuccess,
  });

  async function handleConfirmationModalSuccess() {
    await deleteColumn(data.id);

    client.setQueryData<ExpandedBoard>(["board", boardId], (prev) => {
      if (prev == undefined) {
        throw new Error("Cache invalido");
      }

      const columnIdx = prev.columns.findIndex((x) => x.id === data.id);
      prev.columns.splice(columnIdx, 1);

      return prev;
    });

    closeColumnModal();
  }

  function handleColumnModalSuccess() {
    closeColumnModal();
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
      if (data.type === 2) {
        item[0].done = true;
      } else {
        item[0].done = false;
      }

      const newColIdx = prev.columns.findIndex((x) => x.id === data.id);
      prev.columns[newColIdx].itens.push(item[0]);
      prev.columns[newColIdx].itemCount += 1;

      itemId = "";

      return prev;
    });
  }

  return (
    <ColumnStyled
      onDragOver={(e) => {
        e.preventDefault();
        onDragOver(data.id);
      }}
      onDrop={() => {
        if (itemId) {
          handleColumnChange();
        }
      }}
      onDragStart={(e) => {
        e.stopPropagation();
        onDragStart(data.id);
      }}
      onDragEnd={onDragEnd}
      draggable
    >
      {columnModal}
      {confirmationModal}
      <ColumnHeading>
        <Text lineLimiter>{data.name}</Text>
        <ColumnActions>
          <TbTrash
            role="button"
            size={24}
            cursor="pointer"
            onClick={openConfirmationModal}
          />
          <TbEdit
            role="button"
            size={24}
            cursor="pointer"
            onClick={openColumnModal}
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
