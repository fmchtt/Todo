import styled from "styled-components";
import { FormBoardProps } from "./types";

export const FormContainer = styled.div<FormBoardProps>`
  max-width: ${(props) => props.maxWidth || "none"};
  width: 100%;
  padding: 30px;

  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: center;
  gap: 10px;
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

export const Input = styled.input`
  background-color: ${(props) => props.theme.colors[200]};
  color: ${(props) => props.theme.font.medium};

  ::placeholder {
    color: ${(props) => props.theme.font.medium};
  }

  outline: none;

  border: none;
  border-radius: 25px;

  padding: 15px 25px;
`;
