import { Text } from "@/assets/css/global.styles";
import { Alert } from "./styles";

interface ErrorMessageProps {
  children: string;
}

export default function ErrorMessage({ children }: ErrorMessageProps) {
  return (
    <Alert>
      <Text>{children}</Text>
    </Alert>
  );
}
