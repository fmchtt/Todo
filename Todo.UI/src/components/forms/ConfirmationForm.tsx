import FilledButton from "../filledButton";
import { Form, InputGroup, Label } from "./styles";

type ConfirmationModalProps = {
  label: string;
  onConfirm: () => void;
};
export default function ConfirmationModal(props: ConfirmationModalProps) {
  return (
    <Form
      onSubmit={(e) => {
        e.preventDefault();
        props.onConfirm();
      }}
    >
      <InputGroup>
        <Label>{props.label}</Label>
      </InputGroup>
      <FilledButton type="submit">Confirmar</FilledButton>
    </Form>
  );
}
