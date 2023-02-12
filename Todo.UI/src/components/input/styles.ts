import styled from "styled-components";
import { StyledButtonProps, StyledInputProps } from "./types";

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
  display: flex;
  flex-direction: column;
  gap: 2px;
`;

export const InputStyled = styled.input<StyledInputProps>`
  background-color: ${(props) => props.theme.colors[200]};
  color: ${(props) => props.theme.font.medium};

  ::placeholder {
    color: ${(props) => props.theme.font.medium};
  }

  border: none;
  border-radius: 20px;

  height: 70px;
  padding: 0 20px;
`;

export const Button = styled.button<StyledButtonProps>`
  border: none;
  border-radius: 20px;

  height: 70px;

  background-color: ${(props) => props.theme.colors[900]};
  color: ${(props) => props.theme.font.medium};

  cursor: pointer;

  :hover {
    background-color: ${(props) => props.theme.colors[900] + "d1"};
  }
`;
