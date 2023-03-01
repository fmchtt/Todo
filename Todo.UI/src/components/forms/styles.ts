import styled from "styled-components";
import { FormBoardProps } from "./types";

export const FormContainer = styled.div<FormBoardProps>`
  max-width: ${(props) => props.maxWidth || "none"};
  width: 100%;
  padding: 30px;
  height: 100%;
  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: center;
  gap: 50px;
  overflow-y: auto;
`;

export const FormHeading = styled.div``;

type FormProps = {
  width?: string;
};
export const Form = styled.form<FormProps>`
  width: ${(props) => props.width || "100%"};

  gap: 10px;
  display: flex;
  flex-direction: column;
  justify-content: center;
`;

export const Label = styled.label`
  color: ${(props) => props.theme.font.medium};
  font-size: 1.2em;
  font-weight: 200;
`;

type InputGroupProps = {
  centralized?: boolean;
};
export const InputGroup = styled.div<InputGroupProps>`
  position: relative;
  display: flex;
  align-items: ${(props) => (props.centralized ? "center" : "unset")};
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
  border-radius: 20px;

  padding: 15px 25px;
`;

export const Select = styled.select`
  background-color: ${(props) => props.theme.colors[200]};
  color: ${(props) => props.theme.font.medium};

  ::placeholder {
    color: ${(props) => props.theme.font.medium};
  }

  outline: none;

  border: none;
  border-radius: 20px;

  padding: 15px 25px;
`;

export const TextArea = styled.textarea`
  background-color: ${(props) => props.theme.colors[200]};
  color: ${(props) => props.theme.font.medium};

  ::placeholder {
    color: ${(props) => props.theme.font.medium};
  }

  outline: none;

  border: none;
  border-radius: 20px;

  padding: 15px 25px;
`;

export const ImagePreview = styled.img`
  width: clamp(200px, 35%, 90%);
  border-radius: 50%;
  aspect-ratio: 1 / 1;
  object-fit: cover;
  position: relative;
  border: 5px solid ${(props) => props.theme.colors[200]};
`;

interface HoverProp {
  hover: boolean;
}

export const HoverImage = styled.div<HoverProp>`
  border-radius: 50%;
  width: clamp(200px, 35%, 90%);
  height: 100%;
  background-color: #0008;
  z-index: 2;
  position: absolute;
  cursor: pointer;
  display: flex;
  opacity: ${(props) => (props.hover ? "1" : "0")};
  align-items: center;
  justify-content: center;
`;
