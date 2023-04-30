import styled from "styled-components";

export const ContainerModal = styled.div`
  background-color: #0008;

  width: 100vw;
  height: 100vh;

  position: absolute;
  top: 0;
  left: 0;
  z-index: 5;

  display: flex;
  justify-content: center;
  align-items: center;
`;

export const ModalStyled = styled.div`
  background-image: ${(props) => props.theme.gradients.semi};
  border-radius: 20px;
  padding: 30px;
`;
