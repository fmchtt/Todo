import { Helmet } from "react-helmet";
import { useQuery } from "react-query";
import { getBoards } from "../../services/api/boards";
import { BoardProps } from "./types";
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
import { useState } from "react";
import Modal from "../../components/modal";
import RegisterBoard from "../../components/forms/RegisterBoard";

export default function Dashboard() {
  const { data, isLoading } = useQuery("boards", getBoards);
  const [modal, setModal] = useState<boolean>(false);
  return (
    <>
      {isLoading ? (
        <h2>Carregando</h2>
      ) : (
        <>
          <Helmet>
            <title>Dashboard</title>
          </Helmet>
          <Container>
            <ButtonAddBoard onClick={() => setModal(true)}>
              <AiOutlinePlus className="icon-plus" />
              <SubtitleBoard>Adicionar Quadro</SubtitleBoard>
            </ButtonAddBoard>
            <Section>
              <TitleSection>Quadros</TitleSection>
              <Carousel>
                {" "}
                {data.map((board: BoardProps) => {
                  const { name } = board;
                  return (
                    <Board>
                      <DivTitle>
                        <TitleBoard>{name}</TitleBoard>
                        <SubtitleBoard>
                          Quadro do projeto frontend
                        </SubtitleBoard>
                      </DivTitle>
                      <DivStatus>
                        <Status>
                          <StatusSingle>
                            <SubtitleBoard>Concluídos</SubtitleBoard>
                            <Counter>2/10</Counter>
                          </StatusSingle>
                          <StatusSingle>
                            <SubtitleBoard>Abertos</SubtitleBoard>
                            <Counter>2/10</Counter>
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
        </>
      )}
      {modal && (
        <Modal>
          <RegisterBoard
            maxWidth="400px"
            borderRadius="5px"
            closeModal={() => {
              setModal(false);
            }}
          />
        </Modal>
      )}
    </>
  );
}
