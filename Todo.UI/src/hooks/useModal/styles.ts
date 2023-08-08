import styled from "styled-components";

type ContainerProps = {
  closing: boolean;
};
export const ContainerModal = styled.div<ContainerProps>`
  background-color: #0008;

  width: 100vw;
  height: 100vh;

  padding: 30px;

  position: absolute;
  top: 0;
  left: 0;
  z-index: 5;

  display: flex;
  justify-content: center;
  align-items: center;

  overflow-y: auto;

  ${(props) => {
    return props.closing
      ? "animation: fade-out 200ms linear;"
      : "animation: fade-in 200ms linear;";
  }}
`;

export const ModalStyled = styled.div`
  background-image: ${(props) => props.theme.gradients.semi};
  border-radius: 20px;

  padding: 30px;
  margin-top: 150px;
`;
