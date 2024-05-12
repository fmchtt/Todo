import { Text } from "@/assets/css/global.styles";
import { ExpandedColumn } from "@/types/column";
import { ResumedItem } from "@/types/item";
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
import useConfirmationModal from "@/hooks/useConfirmationModal";
import { useColumnDelete } from "@/adapters/columnAdapters";
import { DetailedHTMLProps } from "react";
import { useItemColumnChange } from "@/adapters/itemAdapters";
import { MessageResponse } from "@/types/responses/message";
import { toast } from "react-toastify";

type DivProps = DetailedHTMLProps<
  React.HTMLAttributes<HTMLDivElement>,
  HTMLDivElement
>;
export type ColumnProps = DivProps & {
  data: ExpandedColumn;
  totalItems: number;
  boardId: string;
  onItemClick: (item: ResumedItem) => void;
};
export default function Column({
  data,
  totalItems,
  onItemClick,
  boardId,
  ...props
}: ColumnProps) {
  const columnDeleteMutation = useColumnDelete({
    onSuccess: () => {
      closeConfirmModal();
    },
  });

  const itemUpdateMutation = useItemColumnChange({
    onError: (e) => {
      const response = e.response?.data as MessageResponse | undefined;
      toast.error(
        response?.message ||
          "Erro ao atualizar coluna, tente novamente mais tarde!"
      );
    },
  });

  const [columnModal, openColumnModal, closeColumnModal] = useModal(
    <ColumnForm
      data={{ id: data.id, name: data.name, type: data.type.toString() }}
      boardId={boardId}
      onSuccess={handleColumnModalSuccess}
    />
  );

  const [confirmationModal, openConfirmationModal, closeConfirmModal] =
    useConfirmationModal({
      message: `Tem certeza que deseja apagar a coluna: ${data.name} ?`,
      onConfirm: handleConfirmationModalSuccess,
    });

  async function handleConfirmationModalSuccess() {
    columnDeleteMutation.mutate({ id: data.id, boardId: boardId });
  }

  function handleColumnModalSuccess() {
    closeColumnModal();
  }

  return (
    <ColumnStyled
      {...props}
      onDragOver={(e) => {
        if (!e.dataTransfer.types.includes("item")) return;
        e.preventDefault();
      }}
      onDrop={(e) => {
        itemUpdateMutation.mutate({
          boardId: boardId,
          targetColumnId: data.id,
          targetColumnType: data.type,
          originColumnId: e.dataTransfer.getData("originColumn"),
          itemId: e.dataTransfer.getData("id"),
        });
      }}
    >
      {columnModal}
      {confirmationModal}
      <ColumnHeading>
        <Text $lineLimiter>{data.name}</Text>
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
              onDrag={(e) => {
                e.stopPropagation();
              }}
              onDragStart={(e) => {
                e.stopPropagation();
                e.dataTransfer.setData("item", "item");
                e.dataTransfer.setData("id", item.id);
                e.dataTransfer.setData("originColumn", data.id);
              }}
            />
          );
        })}
      </ColumnBody>
    </ColumnStyled>
  );
}
