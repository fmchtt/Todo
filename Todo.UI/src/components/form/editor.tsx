import { Field, FieldProps } from "formik";
import { Description, ErrorMessage, InputGroup, Label } from "./styles";
import { FormInputProps } from "./types";
import { Editor } from "@tinymce/tinymce-react";

export default function TinyMCE({ label, description, name }: FormInputProps) {
  return (
    <Field name={name}>
      {({ field, form, meta }: FieldProps) => (
        <InputGroup>
          <Label>{label}</Label>
          <Editor
            tinymceScriptSrc={`${
              import.meta.env.VITE_API_URL
            }/js/tinymce/tinymce.min.js`}
            init={{
              height: 500,
              plugins: [
                "advlist",
                "autolink",
                "lists",
                "link",
                "image",
                "charmap",
                "anchor",
                "searchreplace",
                "visualblocks",
                "code",
                "fullscreen",
                "insertdatetime",
                "media",
                "table",
                "preview",
                "help",
                "wordcount",
              ],
              language: "pt_BR",
              skin: "oxide-dark",
              content_css: "dark",
              promotion: false,
            }}
            value={field.value}
            onEditorChange={(e) => form.setFieldValue(field.name, e)}
          />
          {description && <Description>{description}</Description>}
          {meta.touched && meta.error && (
            <ErrorMessage>{meta.error}</ErrorMessage>
          )}
        </InputGroup>
      )}
    </Field>
  );
}
