import styled from "styled-components";

export const Container = styled.main`
  background: ${(props) => props.theme.gradients.full};
  height: calc(100vh - 100px);

  display: flex;
  width: 100%;
`;

export const MainContent = styled.div`
  flex: 2;
  height: 100%;
  padding: 30px;

  overflow-y: auto;
`;
