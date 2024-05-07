import styled from "styled-components";
import { ComponentPropsWithoutRef } from "react";
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
  gap: 30px;
  overflow-y: auto;
`;

export const FormHeading = styled.div``;
export const FormFooter = styled.div``;

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
  font-size: 16px;
  font-weight: 200;
`;

type InputGroupProps = {
  $centralized?: boolean;
  $row?: boolean;
  $gap?: number;
};
export const InputGroup = styled.div<InputGroupProps>`
  position: relative;
  display: flex;
  align-items: ${(props) => (props.$centralized ? "center" : "unset")};
  flex-direction: ${(props) => (props.$row ? "row" : "column")};
  gap: ${(props) => (props.$gap ? props.$gap + "px" : "2px")};

  .eye {
    position: absolute;
    top: 40px;
    right: 20px;
    font-size: 25px;
    color: #ffffff;
    cursor: pointer;
  }
`;

type InputProps = ComponentPropsWithoutRef<"input"> & {
  $flexible?: boolean;
};
export const Input = styled.input<InputProps>`
  background-color: ${(props) => props.theme.colors[200]};
  color: ${(props) => props.theme.font.medium};

  ::placeholder {
    color: ${(props) => props.theme.font.medium};
  }

  outline: none;

  border: none;
  border-radius: 20px;

  padding: 15px 25px;

  ${(props) => (props.$flexible ? "flex: 1;" : "")}
`;

export const ImagePreview = styled.img`
  width: clamp(200px, 35%, 90%);
  border-radius: 50%;
  aspect-ratio: 1 / 1;
  object-fit: cover;
  position: relative;
  border: 5px solid ${(props) => props.theme.colors[200]};
`;

interface GroupProps {
  justify?: "center" | "unset" | "space-around" | "space-between";
  align?: "center" | "unset";
  gap?: number;
}

export const Group = styled.div<GroupProps>`
  display: flex;
  align-items: ${(props) => (props.align ? props.align : "unset")};
  justify-content: ${(props) => (props.justify ? props.justify : "unset")};
  gap: ${(props) => (props.gap ? props.gap + "px" : "10px")};
  color: ${(props) => props.theme.font.medium};
`;

interface HoverProps {
  $hover: boolean;
}

export const HoverImage = styled.div<HoverProps>`
  border-radius: 50%;
  width: clamp(200px, 35%, 90%);
  height: 100%;
  background-color: #0008;
  z-index: 2;
  position: absolute;
  cursor: pointer;
  display: flex;
  opacity: ${(props) => (props.$hover ? "1" : "0")};
  align-items: center;
  justify-content: center;
`;
