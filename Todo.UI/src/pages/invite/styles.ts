import styled from "styled-components";

export const Container = styled.div`
  background: ${(props) => props.theme.gradients.full};
  height: calc(100vh - 60px);

  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
`;
