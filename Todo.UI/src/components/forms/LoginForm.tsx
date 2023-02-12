import React from "react";
import { Container } from "./styles";
import Input from "../inputs";

const LoginForm = () => {
  return (
    <Container>
      <h1>Login</h1>
      <p>Faça login para acessar o app</p>
      <Input
        name="email"
        type="text"
        value={""}
        onChange={() => {}}
        placeholder="Ex: nome@email.com"
      />
    </Container>
  );
};

export default LoginForm;
