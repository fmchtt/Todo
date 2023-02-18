import styled from "styled-components";
import { SpinnerProps } from "./types";

export const StyledSpinner = styled.svg<SpinnerProps>`
  animation: rotate 2s linear infinite;
  margin: ${(props) => props.margin || "0"};
  width: ${(props) => props.size || "20px"};
  height: ${(props) => props.size || "20px"};

  & .path {
    stroke-linecap: round;
    animation: dash 1.5s ease-in-out infinite;
  }

  @keyframes rotate {
    100% {
      transform: rotate(360deg);
    }
  }

  @keyframes dash {
    0% {
      stroke-dasharray: 1, 150;
      stroke-dashoffset: 0;
    }
    50% {
      stroke-dasharray: 90, 150;
      stroke-dashoffset: -35;
    }
    100% {
      stroke-dasharray: 90, 150;
      stroke-dashoffset: -124;
    }
  }
`;
