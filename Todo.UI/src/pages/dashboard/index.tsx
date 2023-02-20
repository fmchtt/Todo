import { Helmet } from "react-helmet";
import { useQuery } from "react-query";
import { getBoards } from "../../services/api/boards";
import {
  Container,
  Section,
  Carousel,
  ActionContainer,
  HeadingContainer,
  ActionButton,
} from "./styles";
import { TbPlus } from "react-icons/tb";
import { useModal } from "../../hooks";
import BoardRegister from "../../components/forms/RegisterBoard";
import { Text } from "../../assets/css/global.styles";
import BoardCard from "../../components/boardCard";
import { getItens } from "../../services/api/itens";
import ItemCard from "../../components/itemCard";
import { useState } from "react";
import { Item } from "../../types/item";
import ItemPresentation from "../../components/itemPresentation";
import CreateItemForm from "../../components/forms/CreateItemForm";

export default function Dashboard() {
  const boardQuery = useQuery("boards", getBoards);
  const itemQuery = useQuery("itens", getItens);
  const [itemClicked, setItemClicked] = useState<Item>({} as Item);
  const [handleBoardModal, boardModal] = useModal(<BoardRegister />);
  const [handleItemModal, itemModal] = useModal(
    <ItemPresentation data={itemClicked} onCloseClick={handleItemCloseClick} />,
    false,
    false
  );
  const [handleCreateItemModal, createItemModal] = useModal(
    <CreateItemForm onSucess={handleCreateItemSuccess} />
  );

  function handleCreateItemSuccess() {
    handleCreateItemModal();
  }

  function handleItemCloseClick() {
    handleItemModal();
  }

  return (
    <Container>
      <Helmet>
        <title>Dashboard</title>
      </Helmet>
      {boardModal}
      {itemModal}
      {createItemModal}
      <HeadingContainer>
        <Text size="large">Quadros</Text>
        <ActionContainer>
          <ActionButton onClick={handleBoardModal}>
            <TbPlus size={28} />
            <Text>Adicionar Quadro</Text>
          </ActionButton>
        </ActionContainer>
      </HeadingContainer>
      <Section>
        <Carousel>
          {boardQuery.data?.map((board) => {
            return <BoardCard key={board.id} data={board} />;
          })}
        </Carousel>
      </Section>
      <Section>
        <HeadingContainer>
          <Text size="large" margin="20px 0 0 0">
            Tarefas
          </Text>
          <ActionContainer>
            <ActionButton onClick={handleCreateItemModal}>
              <TbPlus size={28} />
              <Text>Adicionar Tarefa</Text>
            </ActionButton>
          </ActionContainer>
        </HeadingContainer>
        <Carousel>
          {itemQuery.data?.map((item) => {
            return (
              <ItemCard
                key={item.id}
                data={item}
                onClick={() => {
                  setItemClicked(item);
                  handleItemModal();
                }}
              />
            );
          })}
        </Carousel>
      </Section>
    </Container>
  );
}
