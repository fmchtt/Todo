import styled from "styled-components";

export const Container = styled.div`
  background-image: ${(props) => props.theme.gradients.full};
  width: 100vw;
  height: 100vh;
  display: flex;
  justify-content: center;
  align-items: center;
`;
