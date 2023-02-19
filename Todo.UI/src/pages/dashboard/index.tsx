import { Helmet } from "react-helmet";
import { useQuery } from "react-query";
import { getBoards } from "../../services/api/boards";
import {
  Container,
  Section,
  TitleSection,
  Board,
  TitleBoard,
  SubtitleBoard,
  DivTitle,
  DivStatus,
  Status,
  StatusSingle,
  Counter,
  ButtonAddBoard,
  Carousel,
} from "./styles";
import { AiOutlinePlus } from "react-icons/ai";
import { useModal } from "../../hooks";
import BoardRegister from "../../components/forms/RegisterBoard";

export default function Dashboard() {
  const { data } = useQuery("boards", getBoards);
  const [handleModal, modal] = useModal(<BoardRegister />);

  return (
    <Container>
      <Helmet>
        <title>Dashboard</title>
      </Helmet>
      {modal}
      <ButtonAddBoard onClick={handleModal}>
        <AiOutlinePlus className="icon-plus" />
        <SubtitleBoard>Adicionar Quadro</SubtitleBoard>
      </ButtonAddBoard>
      <Section>
        <TitleSection>Quadros</TitleSection>
        <Carousel>
          {data?.map((board) => {
            return (
              <Board>
                <DivTitle>
                  <TitleBoard>{board.name}</TitleBoard>
                  <SubtitleBoard>{board.description}</SubtitleBoard>
                </DivTitle>
                <DivStatus>
                  <Status>
                    <StatusSingle>
                      <SubtitleBoard>Concluídos</SubtitleBoard>
                      <Counter>
                        {board.doneItemCount} / {board.itemCount}
                      </Counter>
                    </StatusSingle>
                    <StatusSingle>
                      <SubtitleBoard>Abertos</SubtitleBoard>
                      <Counter>
                        {board.itemCount - board.doneItemCount} /{" "}
                        {board.itemCount}
                      </Counter>
                    </StatusSingle>
                  </Status>
                  <SubtitleBoard>Ultimo acesso: 10min</SubtitleBoard>
                </DivStatus>
              </Board>
            );
          })}
        </Carousel>
      </Section>
    </Container>
  );
}
