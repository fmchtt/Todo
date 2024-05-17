import { useNavigate, useParams } from "react-router-dom";
import {
  ActionsContainer,
  ColumnContainer,
  ColumnDroppableArea,
  Container,
  HeadingContainer,
} from "./styles";
import { H2, Text } from "@/assets/css/global.styles";
import Column from "@/components/column";
import { Helmet } from "react-helmet";
import { useModal } from "@/hooks";
import ItemPresentation from "@/components/itemPresentation";
import { useState, useEffect, Fragment } from "react";
import { ResumedItem } from "@/types/item";
import CreateItem from "@/components/forms/CreateItemForm";
import { TbEdit, TbPlus, TbTrash } from "react-icons/tb";
import BoardRegister from "@/components/forms/RegisterBoard";
import useConfirmationModal from "@/hooks/useConfirmationModal";
import ColumnForm from "@/components/forms/ColumnForm";
import useAuth from "@/context/auth";
import ParticipantWrapper from "@/components/participantWrapper";
import InviteForm from "@/components/forms/InviteForm";
import { toast } from "react-toastify";
import { useBoard, useBoardDelete } from "@/adapters/boardAdapters";
import { useColumnUpdate } from "@/adapters/columnAdapters";
import { MessageResponse } from "@/types/responses/message";

type BoardProps = {
  id: string;
};
export default function Board() {
  const params = useParams<BoardProps>();
  const navigate = useNavigate();

  const { data, isLoading } = useBoard(params.id);
  const deleteBoardMutation = useBoardDelete({
    onSuccess: () => {
      toast.success("Quadro deletado com sucesso!");
      navigate("/home");
    },
    onError: () => {
      toast.error("Quadro não deletado, tente novamente mais tarde!");
    },
  });
  const updateColumnMutation = useColumnUpdate({
    onError: (e) => {
      const response = e.response?.data as MessageResponse | undefined;
      toast.error(
        response?.message ||
          "Erro ao atualizar coluna, tente novamente mais tarde!"
      );
    },
  });

  const { user } = useAuth();

  const [itemClicked, setItemClicked] = useState<string | undefined>();
  const [isDragging, setDragging] = useState<number | undefined>();

  useEffect(() => {
    if (itemClicked !== undefined) {
      openItemModal();
    }
  }, [itemClicked]);

  const [itemModal, openItemModal, closeItemModal] = useModal(
    itemClicked && (
      <ItemPresentation id={itemClicked} onCloseClick={handleItemCloseClick} />
    ),
    false
  );
  const [createItemModal, openCreateItemModal, closeCreateItemModal] = useModal(
    <CreateItem
      boardId={data?.id}
      onSuccess={handleCreateItemSuccess}
      columns={data?.columns
        .slice()
        .sort((x, y) => (x.order > y.order ? 1 : -1))
        .map((column) => {
          return { label: column.name, value: column.id };
        })}
    />
  );
  const [boardModal, openBoardModal, closeBoardModal] = useModal(
    <BoardRegister
      data={{
        id: data?.id || "",
        description: data?.description,
        name: data?.name,
      }}
      closeModal={handleBoardModalSuccess}
    />
  );
  const [confirmationModal, openConfirmationModal] = useConfirmationModal({
    message: `Tem certeza que deseja apagar o quadro: ${data?.name} ?`,
    onConfirm: handleBoardDelete,
  });
  const [columnModal, openColumnModal, closeColumnModal] = useModal(
    <ColumnForm boardId={data?.id || ""} onSuccess={handleColumnModalSuccess} />
  );

  const [inviteModal, openInviteModal] = useModal(
    <InviteForm
      participants={data?.participants}
      boardId={data?.id || ""}
      ownerId={data?.owner}
    />
  );

  function handleColumnModalSuccess() {
    closeColumnModal();
  }

  function handleBoardDelete() {
    if (!data) {
      return;
    }
    deleteBoardMutation.mutate(data.id);
  }

  function handleItemCloseClick() {
    setItemClicked(undefined);
    closeItemModal();
  }

  function handleCreateItemSuccess() {
    closeCreateItemModal();
  }

  function handleBoardModalSuccess() {
    closeBoardModal();
  }

  return (
    <Container>
      {boardModal}
      {itemModal}
      {inviteModal}
      {createItemModal}
      {confirmationModal}
      {columnModal}
      {!isLoading && data && (
        <Helmet>
          <title>Quadro - {data?.name} - Taskerizer</title>
        </Helmet>
      )}
      <HeadingContainer>
        <H2>{data?.name}</H2>
        {data?.participants && (
          <ParticipantWrapper
            participants={data.participants}
            onClick={openInviteModal}
          />
        )}
        {data && (
          <ActionsContainer>
            <ActionsContainer $clickable onClick={openCreateItemModal}>
              <TbPlus role="button" size={28} />
              <Text>Adicionar Tarefa</Text>
            </ActionsContainer>
            <ActionsContainer $clickable onClick={openColumnModal}>
              <TbPlus role="button" size={28} />
              <Text>Adicionar Coluna</Text>
            </ActionsContainer>
            <TbEdit
              role="button"
              size={28}
              cursor="pointer"
              onClick={openBoardModal}
            />
            {data?.owner === user?.id && (
              <TbTrash
                role="button"
                size={28}
                cursor="pointer"
                onClick={openConfirmationModal}
              />
            )}
          </ActionsContainer>
        )}
      </HeadingContainer>
      <ColumnContainer>
        {data?.columns
          ?.slice()
          ?.sort((x, y) => (x.order > y.order ? 1 : -1))
          ?.map((column, index) => {
            return (
              <Fragment key={column.id}>
                <ColumnDroppableArea
                  $show={!!isDragging && isDragging !== index}
                  $hover={false}
                  onDragOver={(e) => {
                    if (!e.dataTransfer.types.includes("column")) return;
                    e.preventDefault();
                  }}
                  onDrop={(e) => {
                    e.preventDefault();
                    updateColumnMutation.mutate({
                      id: e.dataTransfer.getData("id"),
                      boardId: data.id,
                      order: index,
                    });
                  }}
                />
                <Column
                  draggable
                  onDrag={() => {
                    setDragging(index);
                  }}
                  onDragStart={(e) => {
                    e.stopPropagation();
                    e.dataTransfer.setData("id", column.id);
                    e.dataTransfer.setData("column", "column");
                  }}
                  onDragEnd={(e) => {
                    e.preventDefault();
                    setDragging(undefined);
                  }}
                  onItemClick={(item: ResumedItem) => {
                    setItemClicked(item.id);
                  }}
                  totalItems={data.itemCount}
                  data={column}
                  boardId={data.id}
                />
              </Fragment>
            );
          })}
      </ColumnContainer>
    </Container>
  );
}
