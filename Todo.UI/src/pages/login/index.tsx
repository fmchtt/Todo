import { Container } from "./styles";
import { Side, ContentTextSide } from "./styles";
import LoginForm from "../../components/forms/LoginForm";
import image from "../../assets/images/image-side.png";

const Login = () => {
  return (
    <Container>
      <Side>
        <ContentTextSide>
          <h1>Olá,</h1>
          <h3>Bem-vindos ao Taskerizer</h3>
          <p>Aqui você pode gerenciar seu dia da melhor forma possivel!</p>
        </ContentTextSide>
        <img src={image} />
      </Side>
      <LoginForm />
    </Container>
  );
};

export default Login;
