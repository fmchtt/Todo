import { useQuery, useQueryClient } from "react-query";
import { useNavigate, useParams } from "react-router-dom";
import { getBoardById } from "@/services/api/boards";
import {
  ActionsContainer,
  ColumnContainer,
  Container,
  HeadingContainer,
} from "./styles";
import { H2, Text } from "@/assets/css/global.styles";
import Column from "@/components/column";
import { Helmet } from "react-helmet";
import { useModal } from "@/hooks";
import ItemPresentation from "@/components/itemPresentation";
import { useState } from "react";
import { Item } from "@/types/item";
import CreateItem from "@/components/forms/CreateItemForm";
import { TbEdit, TbPlus, TbTrash } from "react-icons/tb";
import { deleteBoardById } from "@/services/api/boards";
import BoardRegister from "@/components/forms/RegisterBoard";
import useConfirmationModal from "@/hooks/useConfirmationModal";
import ColumnForm from "@/components/forms/ColumnForm";
import { ExpandedBoard } from "@/types/board";
import { ExpandedColumn } from "@/types/column";
import { editColumn } from "@/services/api/column";
import useAuth from "@/context/auth";
import ParticipantWrapper from "@/components/participantWrapper";
import InviteForm from "@/components/forms/InviteForm";

type ParamProps = {
  id: string;
};
export default function Board() {
  const params = useParams<ParamProps>();
  const navigate = useNavigate();
  const { data, isLoading } = useQuery(["board", params.id], getBoardById);
  const client = useQueryClient();
  const { user } = useAuth();

  const [itemClicked, setItemClicked] = useState<Item>({} as Item);
  const handleItemModal = useModal(
    <ItemPresentation
      data={itemClicked}
      onCloseClick={handleItemCloseClick}
      boardId={data?.id}
    />,
    false
  );
  const handleCreateItemModal = useModal(
    <CreateItem
      boardId={data?.id}
      onSuccess={handleCreateItemSuccess}
      columns={data?.columns.map((column) => {
        return { label: column.name, value: column.id };
      })}
    />
  );
  const handleBoardModal = useModal(
    <BoardRegister
      data={{
        id: data?.id || "",
        description: data?.description,
        name: data?.name,
      }}
      closeModal={handleBoardModalSuccess}
    />
  );
  const handleConfirmation = useConfirmationModal({
    message: `Tem certeza que deseja apagar o quadro: ${data?.name} ?`,
    onConfirm: handleBoardDelete,
  });
  const handleColumnModal = useModal(
    <ColumnForm boardId={data?.id || ""} onSuccess={handleColumnModalSuccess} />
  );

  const handleInviteModal = useModal(
    <InviteForm
      participants={data?.participants}
      boardId={data?.id || ""}
      ownerId={data?.owner}
    />
  );

  function handleColumnModalSuccess() {
    handleColumnModal();
  }

  function handleBoardDelete() {
    if (!data) {
      return;
    }
    deleteBoardById(data.id).then(() => {
      navigate("/home");
    });
  }

  function handleItemCloseClick() {
    handleItemModal();
  }

  function handleCreateItemSuccess() {
    handleCreateItemModal();
  }

  function handleBoardModalSuccess() {
    handleBoardModal();
  }

  let dragColumnId = "";
  let overColumnId = "";

  function handleColumnDrag() {
    if (dragColumnId === "" || overColumnId === "") {
      return;
    }

    try {
      client.setQueryData<ExpandedBoard>(["board", data?.id], (prev) => {
        if (prev == undefined) {
          throw new Error("Cache invalido!");
        }

        if (dragColumnId === overColumnId) {
          throw new Error("Colunas iguais!");
        }

        const columnIdx = prev.columns.findIndex((x) => x.id === dragColumnId);
        if (columnIdx < 0) {
          throw new Error("Coluna movida nao encontrada!");
        }

        const items = prev.columns.splice(columnIdx, 1);

        if (items.length === 0) {
          throw new Error("Coluna nao encontrada no quadro!");
        }

        const overColumnIdx = prev.columns.findIndex(
          (x) => x.id === overColumnId
        );

        if (columnIdx < 0) {
          throw new Error("Coluna alvo nao encontrada!");
        }

        const newColumnOrder: ExpandedColumn[] = [];
        for (let i = 0; i < prev.columns.length; i++) {
          if (i === overColumnIdx) {
            newColumnOrder.push(items[0]);
          }
          newColumnOrder.push(prev.columns[i]);
        }

        prev.columns = newColumnOrder;
        for (let i = 0; i < prev.columns.length; i++) {
          prev.columns[i].order = i;
        }

        editColumn({
          id: dragColumnId,
          order: prev.columns[overColumnIdx].order,
        });

        dragColumnId = "";
        overColumnId = "";

        return prev;
      });
    } catch (e) {
      console.log();
    }
  }

  return (
    <Container>
      {!isLoading && data && (
        <Helmet>
          <title>Quadro - {data?.name}</title>
        </Helmet>
      )}
      <HeadingContainer>
        <H2>{data?.name}</H2>
        {data?.participants && (
          <ParticipantWrapper
            participants={data.participants}
            onClick={handleInviteModal}
          />
        )}
        {data && (
          <ActionsContainer>
            <ActionsContainer clickable onClick={handleCreateItemModal}>
              <TbPlus role="button" size={28} />
              <Text>Adicionar Tarefa</Text>
            </ActionsContainer>
            <ActionsContainer clickable onClick={handleColumnModal}>
              <TbPlus role="button" size={28} />
              <Text>Adicionar Coluna</Text>
            </ActionsContainer>
            <TbEdit
              role="button"
              size={28}
              cursor="pointer"
              onClick={handleBoardModal}
            />
            {data?.owner === user?.id && (
              <TbTrash
                role="button"
                size={28}
                cursor="pointer"
                onClick={handleConfirmation}
              />
            )}
          </ActionsContainer>
        )}
      </HeadingContainer>
      <ColumnContainer>
        {data?.columns
          .sort((x, y) => (x.order > y.order ? 1 : -1))
          .map((column) => {
            return (
              <Column
                onItemClick={(item: Item) => {
                  setItemClicked(item);
                  handleItemModal();
                }}
                key={column.id}
                totalItems={data.itemCount}
                data={column}
                boardId={data.id}
                onDragStart={(columnId) => {
                  dragColumnId = columnId;
                }}
                onDragOver={(columnId) => {
                  overColumnId = columnId;
                }}
                onDragEnd={handleColumnDrag}
              />
            );
          })}
      </ColumnContainer>
    </Container>
  );
}
