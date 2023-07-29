import styled from "styled-components";

export const Container = styled.div`
  display: flex;
  flex-direction: column;
  gap: 30px;
  height: 100%;
`;

type EmptyContentProps = {
  card?: boolean;
};
export const EmptyContent = styled.div<EmptyContentProps>`
  width: ${(props) => (props.card ? "260px" : "100%")};
  height: 100px;
  display: flex;
  justify-content: center;
  align-items: center;
  border: ${(props) => props.theme.font.medium} 1px solid;
  border-radius: 20px;
  padding: 10px;
`;

export const TaskTypeContainer = styled.div``;

export const BoardListContainer = styled.div`
  display: flex;
  flex-direction: column;
  margin-top: 5px;
  gap: 30px;
`;
