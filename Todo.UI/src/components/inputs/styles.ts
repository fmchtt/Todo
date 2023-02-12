import styled from "styled-components";

interface Input {
  width?: string;
}

export const InputStyle = styled.div<Input>`
  width: ${(props) => props.width || "calc(100% - 30px)"};
  padding: 10px 15px;
  height: 55px;
  border-radius: 5px;
  outline: none;
  border: none;
  background-color: #00a9a5;
  color: #fff;
`;
