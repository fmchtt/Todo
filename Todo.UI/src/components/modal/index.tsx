import { ContainerModal } from "./styles";

interface Children {
  children?: any;
}

export default function Modal(props: Children) {
  return <ContainerModal>{props.children}</ContainerModal>;
}
