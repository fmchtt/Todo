import { Field, FieldProps } from "formik";
import {
  Description,
  ErrorMessage,
  InputGroup,
  Label,
  StyledInput,
} from "./styles";
import { FormInputProps } from "./types";
import { useState } from "react";
import { TbEye, TbEyeOff } from "react-icons/tb";

export default function Password({
  label,
  description,
  name,
  placeholder,
}: FormInputProps) {
  const [showPassword, setShowPassword] = useState("password");

  function eyeInput() {
    if (showPassword === "password") {
      setShowPassword("text");
    } else {
      setShowPassword("password");
    }
  }

  return (
    <Field name={name}>
      {({ field, form, meta }: FieldProps) => (
        <InputGroup>
          <Label>{label}</Label>
          <StyledInput
            {...field}
            type={showPassword}
            placeholder={placeholder}
            onChange={form.handleChange}
          />
          {showPassword === "password" ? (
            <TbEye className="eye" onClick={eyeInput} />
          ) : (
            <TbEyeOff onClick={eyeInput} className="eye" />
          )}
          {description && <Description>{description}</Description>}
          {meta.touched && meta.error && (
            <ErrorMessage>{meta.error}</ErrorMessage>
          )}
        </InputGroup>
      )}
    </Field>
  );
}
