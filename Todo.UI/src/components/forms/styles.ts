import styled from "styled-components";
import { FormBoardProps } from "./types";

export const FormContainer = styled.div<FormBoardProps>`
  background-image: ${(props) => props.theme.gradients.semi};
  max-width: ${(props) => props.maxWidth || "none"};
  width: 100%;
  padding: 30px;
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 10px;
  justify-content: center;
  position: relative;
  top: ${(props) => (props.maxWidth ? "50%" : "none")};
  left: ${(props) => (props.maxWidth ? "50%" : "none")};
  transform: ${(props) => (props.maxWidth ? "translate(-50%, -50%)" : "none")};
  border-radius: ${(props) => props.borderRadius || "none"};

  .icon-close {
    font-size: 30px;
    color: #fff;
    cursor: pointer;
  }
`;

export const Form = styled.form`
  width: 100%;

  gap: 10px;
  display: flex;
  flex-direction: column;
  justify-content: center;

  margin: 30px 0;
`;

export const Label = styled.label`
  color: ${(props) => props.theme.font.medium};
  font-size: 1.2em;
  font-weight: 200;
`;

export const InputGroup = styled.div`
  position: relative;
  display: flex;
  flex-direction: column;
  gap: 2px;

  .eye {
    position: absolute;
    top: 44px;
    right: 20px;
    font-size: 25px;
    color: #ffffff;
    cursor: pointer;
  }
`;
