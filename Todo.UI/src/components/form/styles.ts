import { ComponentPropsWithoutRef } from "react";
import styled from "styled-components";

type InputGroupProps = {
  $centralized?: boolean;
  row?: boolean;
  gap?: number;
};
export const InputGroup = styled.div<InputGroupProps>`
  position: relative;
  display: flex;
  flex: 1;
  align-items: ${(props) => (props.$centralized ? "center" : "unset")};
  flex-direction: ${(props) => (props.row ? "row" : "column")};
  gap: ${(props) => (props.gap ? props.gap + "px" : "2px")};

  .eye {
    position: absolute;
    top: 40px;
    right: 20px;
    font-size: 25px;
    color: #ffffff;
    cursor: pointer;
  }
`;

export const Label = styled.label`
  color: ${(props) => props.theme.font.medium};
  font-size: 16px;
  font-weight: 200;
`;

export const Description = styled.span`
  color: ${(props) => props.theme.font.medium};
  font-size: 14px;
  font-weight: 100;
`;

export const ErrorMessage = styled.span`
  color: #e65555;
  font-weight: 400;
  font-size: 15px;
`;

export type FormProps = {
  width?: string;
  title?: string;
};
export const FormContainer = styled.form<FormProps>`
  width: ${(props) => props.width || "100%"};

  gap: 10px;
  display: flex;
  flex-direction: column;
  justify-content: center;
`;

type InputProps = ComponentPropsWithoutRef<"input"> & {
  flexible?: boolean;
};
export const StyledInput = styled.input<InputProps>`
  background-color: ${(props) => props.theme.colors[200]};
  color: ${(props) => props.theme.font.medium};

  &::placeholder {
    color: ${(props) => props.theme.font.medium};
  }

  outline: none;

  border: none;
  border-radius: 20px;

  padding: 15px 25px;

  ${(props) => (props.flexible ? "flex: 1;" : "")}
`;

export const StyledSelect = styled.select`
  background-color: ${(props) => props.theme.colors[200]};
  color: ${(props) => props.theme.font.medium};

  &::placeholder {
    color: ${(props) => props.theme.font.medium};
  }

  outline: none;

  border: none;
  border-radius: 20px;

  padding: 15px 25px;
`;

export const StyledTextArea = styled.textarea`
  background-color: ${(props) => props.theme.colors[200]};
  color: ${(props) => props.theme.font.medium};

  &::placeholder {
    color: ${(props) => props.theme.font.medium};
  }

  outline: none;

  border: none;
  border-radius: 20px;

  padding: 15px 25px;
`;
