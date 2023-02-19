import { Text } from "../../../assets/css/global.styles";

interface ErrorMessageProps {
  children: string;
}

export default function ErrorMessage({ children }: ErrorMessageProps) {
  return <Text>{children}</Text>;
}
