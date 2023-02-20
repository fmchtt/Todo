import { useQuery } from "react-query";
import { useParams } from "react-router-dom";
import { getBoardById } from "../../services/api/boards";
import {
  ActionsContainer,
  ColumnContainer,
  Container,
  HeadingContainer,
} from "./styles";
import { H2 } from "../../assets/css/global.styles";
import Column from "../../components/column";
import { TiPlusOutline, TiEdit, TiTrash } from "react-icons/ti";
import { Helmet } from "react-helmet";
import { useModal } from "../../hooks";
import ItemPresentation from "../../components/itemPresentation";
import { useState } from "react";
import { Item } from "../../types/item";
import CreateItem from "../../components/forms/CreateItemForm";

type ParamProps = {
  id: string;
};
export default function Board() {
  const params = useParams<ParamProps>();
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

  function handleItemCloseClick() {
    handleItemModal();
  }

  function handleCreateItemSuccess() {
    handleCreateItemModal();
  }

  return (
    <Container>
      {!isLoading && (
        <Helmet>
          <title>Quadro - {data?.name}</title>
        </Helmet>
      )}
      {itemModal}
      {createItemModal}
      <HeadingContainer>
        <H2>{data?.name}</H2>
        <ActionsContainer>
          <TiPlusOutline
            role="button"
            size={30}
            cursor="pointer"
            onClick={handleCreateItemModal}
          />
          <TiEdit role="button" size={30} cursor="pointer" />
          <TiTrash role="button" size={30} cursor="pointer" />
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
