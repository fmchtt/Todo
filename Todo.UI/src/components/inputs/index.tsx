import React from "react";
import { InputStyle } from "./styles";

interface InputProps {
  type: string;
  name: string;
  value: any;
  width?: string;
  placeholder: string;
  onChange?: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

const Input = (props: InputProps) => {
  return (
    <InputStyle
      {...props}
      type={props.type}
      name={props.name}
      value={props.value}
      width={props.width}
      placeholder={props.placeholder}
      onChange={props.onChange}
    />
  );
};

export default Input;
