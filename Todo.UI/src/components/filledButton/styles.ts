import styled from "styled-components";
import { StyledButtonProps } from "./types";

export const ButtonStyle = styled.button<StyledButtonProps>`
  border: none;
  border-radius: 20px;

  height: 60px;

  background-color: ${(props) => props.theme.colors[700]};
  color: ${(props) => props.theme.font.medium};
  margin: 23px 0;
  cursor: pointer;
  display: flex;
  justify-content: center;
  align-items: center;

  font-size: 1rem;

  :hover {
    background-color: ${(props) => props.theme.colors[800] + "d1"};
  }
`;
