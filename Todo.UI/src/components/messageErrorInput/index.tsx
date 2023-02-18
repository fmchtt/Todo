import { Text } from "./style";

interface Text {
  children: string;
}

const MessageErrorInput = (props: Text) => {
  return <Text>{props.children}</Text>;
};

export default MessageErrorInput;
