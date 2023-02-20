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

  function handleItemCloseClick() {
    handleItemModal();
  }

  return (
    <Container>
      {!isLoading && (
        <Helmet>
          <title>Quadro - {data?.name}</title>
        </Helmet>
      )}
      {itemModal}
      <HeadingContainer>
        <H2>{data?.name}</H2>
        <ActionsContainer>
          <TiPlusOutline role="button" size={30} cursor="pointer" />
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
