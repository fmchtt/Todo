import { Container } from "./styles";
import { Side, ContentTextSide } from "./styles";
import LoginForm from "../../components/forms/LoginForm";
import image from "../../assets/images/image-side.png";
import { Helmet } from "react-helmet";
import { H1, H2, Text } from "../../assets/css/global.styles";

export default function Login() {
  return (
    <Container>
      <Helmet>
        <title>Login</title>
      </Helmet>
      <Side>
        <ContentTextSide>
          <H1>Olá,</H1>
          <H2>Bem-vindos ao Taskerizer</H2>
          <Text>
            Aqui você pode gerenciar seu dia da melhor forma possivel!
          </Text>
        </ContentTextSide>
        <img src={image} />
      </Side>
      <LoginForm />
    </Container>
  );
}
