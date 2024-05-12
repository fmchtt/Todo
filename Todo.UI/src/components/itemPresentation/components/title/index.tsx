import { useRef, useState } from "react";
import { TitleStyled } from "./styles";
import { InputGroup } from "@/components/form/styles";
import { Input } from "@/components/forms/styles";
import FilledButton from "@/components/filledButton";
import { TbCheck, TbX } from "react-icons/tb";

type TitleProps = {
  title: string;
  onChange: (value: string) => void;
};
export default function Title(props: TitleProps) {
  const [isEditing, setEditing] = useState(false);

  const inputRef = useRef<HTMLInputElement | null>();

  if (isEditing) {
    return (
      <InputGroup $row>
        <Input
          $flexible
          ref={(ref) => (inputRef.current = ref)}
          defaultValue={props.title}
        />
        <FilledButton
          $margin="0"
          onClick={() => {
            setEditing(false);
            props.onChange(inputRef.current?.value || props.title);
          }}
        >
          <TbCheck />
        </FilledButton>
        <FilledButton $margin="0" onClick={() => setEditing(false)}>
          <TbX />
        </FilledButton>
      </InputGroup>
    );
  }

  return (
    <TitleStyled onClick={() => setEditing(true)}>{props.title}</TitleStyled>
  );
}
