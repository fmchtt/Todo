import { Formik, FormikConfig, FormikValues } from "formik";
import { PropsWithChildren } from "react";
import Input from "./input";
import { FormContainer, FormProps } from "./styles";
import TextArea from "./textarea";
import SubmitButton from "./submit";
import { H1 } from "@/assets/css/global.styles";
import Select from "./select";
import Password from "./password";
import TinyMCE from "./editor";

export default function Form<T>({
  children,
  title,
  ...props
}: PropsWithChildren<
  FormikConfig<T extends FormikValues ? T : FormikValues> & FormProps
>) {
  return (
    <Formik {...props}>
      {(formProps) => (
        <FormContainer
          width={props.width}
          onSubmit={formProps.handleSubmit}
          onReset={formProps.handleReset}
        >
          {title && <H1>{title}</H1>}
          {children}
        </FormContainer>
      )}
    </Formik>
  );
}

Form.Input = Input;
Form.Select = Select;
Form.TextArea = TextArea;
Form.Password = Password;
Form.Editor = TinyMCE;
Form.Submit = SubmitButton;
