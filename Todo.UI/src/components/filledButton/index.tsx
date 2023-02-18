import Spinner from "../spinner";
import { ButtonStyle } from "./styles";
import { FilledButtonProps } from "./types";

export default function FilledButton(props: FilledButtonProps) {
  return (
    <ButtonStyle {...props} type={props.type}>
      {props.loading ? <Spinner size={props.size} /> : props.children}
    </ButtonStyle>
  );
}
