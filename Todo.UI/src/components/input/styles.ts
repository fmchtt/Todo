import styled from "styled-components";
import { StyledInputProps } from "./types";

export const InputStyled = styled.input<StyledInputProps>`
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
