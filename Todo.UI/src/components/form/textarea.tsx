import { Field, FieldProps } from "formik";
import {
  Description,
  ErrorMessage,
  InputGroup,
  Label,
  StyledTextArea,
} from "./styles";
import { FormInputProps } from "./types";

type TextAreaProps = React.DetailedHTMLProps<
  React.TextareaHTMLAttributes<HTMLTextAreaElement>,
  HTMLTextAreaElement
>;

export default function TextArea({
  label,
  description,
  name,
  ...props
}: FormInputProps & TextAreaProps) {
  return (
    <Field name={name}>
      {({ field, form, meta }: FieldProps) => (
        <InputGroup>
          <Label>{label}</Label>
          <StyledTextArea {...field} {...props} onChange={form.handleChange} />
          {description && <Description>{description}</Description>}
          {meta.touched && meta.error && (
            <ErrorMessage>{meta.error}</ErrorMessage>
          )}
        </InputGroup>
      )}
    </Field>
  );
}
