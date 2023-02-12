import styled from "styled-components";

export const FormContainer = styled.div`
  background-image: ${(props) => props.theme.gradients.semi};
  width: 100%;
  padding: 30px;
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 10px;
  justify-content: center;
`;
