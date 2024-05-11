import { Helmet } from "react-helmet";
import {
  Container,
  Section,
  Carousel,
  ActionContainer,
  HeadingContainer,
  ActionButton,
} from "./styles";
import { TbPlus } from "react-icons/tb";
import { useModal } from "@/hooks";
import BoardRegister from "@/components/forms/RegisterBoard";
import { Text } from "@/assets/css/global.styles";
import BoardCard from "@/components/boardCard";
import ItemCard from "@/components/itemCard";
import { useState, useEffect } from "react";
import ItemPresentation from "@/components/itemPresentation";
import CreateItemForm from "@/components/forms/CreateItemForm";
import { EmptyContent } from "@/pages/tasks/styles";
import { useBoards } from "@/adapters/boardAdapters";
import { useItems } from "@/adapters/itemAdapters";

export default function Dashboard() {
  const boardQuery = useBoards();
  const itemQuery = useItems();

  const [itemClicked, setItemClicked] = useState<number | undefined>(undefined);
  const [boardModal, openBoardModal, closeBoardModal] = useModal(
    <BoardRegister closeModal={handleCreateBoardSuccess} />
  );
  const [itemModal, openItemModal, closeItemModal] = useModal(
    itemQuery.data && itemClicked !== undefined && (
      <ItemPresentation
        key="modalItem"
        data={itemQuery.data[itemClicked]}
        onCloseClick={handleItemCloseClick}
      />
    ),
    false
  );
  const [createItemModal, openCreateItemModal, closeCreateItemModal] = useModal(
    <CreateItemForm onSuccess={handleCreateItemSuccess} />
  );

  function handleCreateBoardSuccess() {
    closeBoardModal();
  }

  function handleCreateItemSuccess() {
    closeCreateItemModal();
  }

  function handleItemCloseClick() {
    setItemClicked(undefined);
    closeItemModal();
  }

  useEffect(() => {
    if (itemClicked !== undefined) {
      openItemModal();
    }
  }, [itemClicked]);

  return (
    <Container>
      {createItemModal}
      {boardModal}
      {itemModal}
      <Helmet>
        <title>Dashboard - Taskerizer</title>
      </Helmet>
      <HeadingContainer>
        <Text $size="large">Quadros</Text>
        <ActionContainer>
          <ActionButton onClick={openBoardModal}>
            <TbPlus size={28} />
            <Text>Adicionar Quadro</Text>
          </ActionButton>
        </ActionContainer>
      </HeadingContainer>
      <Section>
        <Carousel>
          {boardQuery.data
            ?.map((board) => {
              return <BoardCard key={board.id} data={board} />;
            })
            .slice(0, 4)}
          {boardQuery.data?.length === 0 && (
            <EmptyContent>
              <Text>Você ainda não tem quadros!</Text>
            </EmptyContent>
          )}
        </Carousel>
      </Section>
      <Section>
        <HeadingContainer>
          <Text $size="large" $margin="20px 0 0 0">
            Tarefas
          </Text>
          <ActionContainer>
            <ActionButton onClick={openCreateItemModal}>
              <TbPlus size={28} />
              <Text>Adicionar Tarefa</Text>
            </ActionButton>
          </ActionContainer>
        </HeadingContainer>
        <Carousel>
          {itemQuery.data
            ?.map((item, idx) => {
              return (
                <ItemCard
                  key={item.id}
                  data={item}
                  onClick={() => {
                    setItemClicked(idx);
                  }}
                />
              );
            })
            .slice(0, 4)}
          {itemQuery.data?.length === 0 && (
            <EmptyContent>
              <Text>Você ainda não tem tarefas!</Text>
            </EmptyContent>
          )}
        </Carousel>
      </Section>
    </Container>
  );
}
