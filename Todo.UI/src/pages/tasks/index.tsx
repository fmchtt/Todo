import { Text } from "@/assets/css/global.styles";
import {
  BoardListContainer,
  Container,
  EmptyContent,
  TaskTypeContainer,
} from "./styles";
import TasksGrid from "@/components/tasksGrid";
import { useBoards } from "@/adapters/boardAdapters";

export default function Tasks() {
  const boardQuery = useBoards();

  return (
    <Container>
      <TaskTypeContainer>
        <Text $size="large">Tarefas sem quadro</Text>
        <EmptyContent>
          <Text>Você ainda não tem tarefas sem quadro!</Text>
        </EmptyContent>
      </TaskTypeContainer>
      <TaskTypeContainer>
        <Text $size="large">Tarefas por quadro</Text>
        {boardQuery.data?.length !== 0 ? (
          <BoardListContainer>
            {boardQuery.data?.map((board) => {
              return <TasksGrid key={board.id} board={board}></TasksGrid>;
            })}
          </BoardListContainer>
        ) : (
          <EmptyContent>
            <Text>Você ainda não tem quadros!</Text>
          </EmptyContent>
        )}
      </TaskTypeContainer>
    </Container>
  );
}
