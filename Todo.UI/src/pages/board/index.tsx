import { useQuery } from "react-query";
import { useNavigate, useParams } from "react-router-dom";
import { getBoardById } from "../../services/api/boards";
import {
  ActionsContainer,
  ColumnContainer,
  Container,
  HeadingContainer,
} from "./styles";
import { H2 } from "../../assets/css/global.styles";
import Column from "../../components/column";
import { Helmet } from "react-helmet";
import { useModal } from "../../hooks";
import ItemPresentation from "../../components/itemPresentation";
import { useState } from "react";
import { Item } from "../../types/item";
import CreateItem from "../../components/forms/CreateItemForm";
import { TbEdit, TbPlus, TbTrash } from "react-icons/tb";
import { deleteBoardById } from "../../services/api/boards";
import BoardRegister from "../../components/forms/RegisterBoard";
import useConfirmationModal from "../../hooks/useConfirmationModal";

type ParamProps = {
  id: string;
};
export default function Board() {
  const params = useParams<ParamProps>();
  const navigate = useNavigate();
  const { data, isLoading } = useQuery(["board", params.id], getBoardById);

  const [itemClicked, setItemClicked] = useState<Item>({} as Item);
  const [handleItemModal, itemModal] = useModal(
    <ItemPresentation data={itemClicked} onCloseClick={handleItemCloseClick} />,
    false,
    false
  );
  const [handleCreateItemModal, createItemModal] = useModal(
    <CreateItem
      boardId={data?.id}
      onSucess={handleCreateItemSuccess}
      columns={data?.columns.map((column) => {
        return { label: column.name, value: column.id };
      })}
    />
  );
  const [handleBoardModal, boardModal] = useModal(
    <BoardRegister
      data={{
        id: data?.id || "",
        description: data?.description,
        name: data?.name,
      }}
      closeModal={handleBoardModalSuccess}
    />
  );
  const [handleConfirmation, confirmationModal] = useConfirmationModal({
    message: `Tem certeza que deseja apagar o quadro: ${data?.name} ?`,
    onConfirm: handleBoardDelete,
  });

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

  return (
    <Container>
      {!isLoading && (
        <Helmet>
          <title>Quadro - {data?.name}</title>
        </Helmet>
      )}
      {boardModal}
      {itemModal}
      {createItemModal}
      {confirmationModal}
      <HeadingContainer>
        <H2>{data?.name}</H2>
        <ActionsContainer>
          <TbPlus
            role="button"
            size={28}
            cursor="pointer"
            onClick={handleCreateItemModal}
          />
          <TbEdit
            role="button"
            size={28}
            cursor="pointer"
            onClick={handleBoardModal}
          />
          <TbTrash
            role="button"
            size={28}
            cursor="pointer"
            onClick={handleConfirmation}
          />
        </ActionsContainer>
      </HeadingContainer>
      <ColumnContainer>
        {data?.columns.map((column) => {
          return (
            <Column
              onItemClick={(item: Item) => {
                setItemClicked(item);
                handleItemModal();
              }}
              key={column.id}
              totalItems={data.itemCount}
              data={column}
            />
          );
        })}
      </ColumnContainer>
    </Container>
  );
}
