import { Field, FieldProps } from "formik";
import {
  Description,
  ErrorMessage,
  InputGroup,
  Label,
  StyledSelect,
} from "./styles";
import { FormInputProps } from "./types";

type Option = {
  label: string;
  value: string | number;
};

type SelectProps = {
  options: Option[];
};

export default function Select({
  label,
  description,
  name,
  options,
}: FormInputProps & SelectProps) {
  return (
    <Field name={name}>
      {({ field, form, meta }: FieldProps) => (
        <InputGroup>
          <Label>{label}</Label>
          <StyledSelect {...field} onChange={form.handleChange}>
            {options.map((o) => (
              <option key={`opt_${o.value}_${o.label}`} value={o.value}>
                {o.label}
              </option>
            ))}
          </StyledSelect>
          {description && <Description>{description}</Description>}
          {meta.touched && meta.error && (
            <ErrorMessage>{meta.error}</ErrorMessage>
          )}
        </InputGroup>
      )}
    </Field>
  );
}
