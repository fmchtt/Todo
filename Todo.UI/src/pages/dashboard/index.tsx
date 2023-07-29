import { Helmet } from "react-helmet";
import { useQuery } from "@tanstack/react-query";
import { getBoards } from "@/services/api/boards";
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
import { getItens } from "@/services/api/itens";
import ItemCard from "@/components/itemCard";
import { useState, useEffect } from "react";
import ItemPresentation from "@/components/itemPresentation";
import CreateItemForm from "@/components/forms/CreateItemForm";

export default function Dashboard() {
  const boardQuery = useQuery({
    queryKey: ["boards"],
    queryFn: getBoards,
  });
  const itemQuery = useQuery({
    queryKey: ["itens"],
    queryFn: getItens,
  });
  const [itemClicked, setItemClicked] = useState<number | undefined>(undefined);
  const [boardModal, openBoardModal] = useModal(<BoardRegister />);
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
        <Text size="large">Quadros</Text>
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
        </Carousel>
      </Section>
      <Section>
        <HeadingContainer>
          <Text size="large" margin="20px 0 0 0">
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
        </Carousel>
      </Section>
    </Container>
  );
}
