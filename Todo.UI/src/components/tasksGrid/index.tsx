import { BoardContainer, ItemContainer, TaskTypeContainer } from "./styles";
import { H2, Text } from "@/assets/css/global.styles";
import { ResumedBoard } from "@/types/board";
import ItemCard from "../itemCard";
import { EmptyContent } from "@/pages/tasks/styles";
import { useModal } from "@/hooks";
import ItemPresentation from "../itemPresentation";
import { useState } from "react";
import { useItems } from "@/adapters/itemAdapters";

type TasksGridProps = {
  board: ResumedBoard;
};

export default function TasksGrid(props: TasksGridProps) {
  const [itemClicked, setItemClicked] = useState<number | undefined>();

  const items = useItems(props.board.id);

  function handleItemCloseClick() {
    setItemClicked(undefined);
    closeItemModal();
  }

  const [itemModal, openItemModal, closeItemModal] = useModal(
    items.data && itemClicked !== undefined && (
      <ItemPresentation
        key="modalItem"
        data={items.data[itemClicked]}
        onCloseClick={handleItemCloseClick}
      />
    ),
    false
  );

  const openItens = items.data?.filter((x) => x.done === false);
  const doneItens = items.data?.filter((x) => x.done === true);

  return (
    <BoardContainer>
      {itemModal}
      <H2>{props.board.name}</H2>
      <TaskTypeContainer>
        <Text>Tarefas Abertas</Text>
        {openItens?.length !== 0 ? (
          <ItemContainer>
            {openItens?.map((item) => {
              return (
                <ItemCard
                  key={item.id}
                  data={item}
                  draggable={false}
                  onClick={() => {
                    setItemClicked(
                      items.data?.findIndex((x) => x.id === item.id)
                    );
                    openItemModal();
                  }}
                />
              );
            })}
          </ItemContainer>
        ) : (
          <EmptyContent card>
            <Text>O quadro ainda não tem tarefas abertas!</Text>
          </EmptyContent>
        )}
      </TaskTypeContainer>
      <TaskTypeContainer>
        <Text>Tarefas Encerradas</Text>
        {doneItens?.length !== 0 ? (
          <ItemContainer>
            {doneItens?.map((item) => {
              return (
                <ItemCard
                  key={item.id}
                  data={item}
                  draggable={false}
                  onClick={() => {
                    setItemClicked(
                      items.data?.findIndex((x) => x.id === item.id)
                    );
                    openItemModal();
                  }}
                />
              );
            })}
          </ItemContainer>
        ) : (
          <EmptyContent card>
            <Text>O quadro ainda não tem tarefas encerradas!</Text>
          </EmptyContent>
        )}
      </TaskTypeContainer>
    </BoardContainer>
  );
}
