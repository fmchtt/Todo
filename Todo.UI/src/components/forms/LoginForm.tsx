import { FormContainer } from "./styles";
import Input from "../input";
import { Button, InputGroup, Label, Form } from "../input/styles";
import { H1, Text } from "../../assets/css/global.styles";
import useAuth from "../../context/auth";
import { FormEvent, useEffect } from "react";
import { useNavigate } from "react-router-dom";

export default function LoginForm() {
  const { user, login } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (user) {
      navigate("/home");
    }
  }, [user]);

  async function handleLogin(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();

    const formData = new FormData(e.target as HTMLFormElement);
    await login({
      email: formData.get("email") as string,
      password: formData.get("password") as string,
    });
  }

  return (
    <FormContainer>
      <H1>Login</H1>
      <Text>Faça login para acessar o app</Text>
      <Form onSubmit={handleLogin}>
        <InputGroup>
          <Label>Email</Label>
          <Input type="email" name="email" placeholder="Ex: teste@email.com" />
        </InputGroup>
        <InputGroup>
          <Label>Senha</Label>
          <Input type="password" name="password" placeholder="Ex: Senha1234@" />
        </InputGroup>
        <Button>Entrar</Button>
      </Form>
    </FormContainer>
  );
}
