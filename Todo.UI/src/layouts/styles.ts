import styled from "styled-components";

type ContainerProps = {
  centralize?: boolean;
};
export const Container = styled.main<ContainerProps>`
  background: ${(props) => props.theme.gradients.full};
  height: calc(100vh - 60px);

  display: flex;
  width: 100%;

  align-items: ${(props) => (props.centralize ? "center" : "unset")};
  justify-content: ${(props) => (props.centralize ? "center" : "unset")};
`;

export const MainContent = styled.div`
  flex: 2;
  height: 100%;
  padding: 10px 20px;

  overflow-y: auto;
`;
