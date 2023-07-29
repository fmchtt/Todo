import Spinner from "@/components/spinner";
import { Container } from "./styles";

export default function BaseLoader() {
  return (
    <Container centralize>
      <Spinner />
    </Container>
  );
}
