import http from "@/services/http";
import { FormEvent, useState } from "react";
import FilledButton from "../filledButton";
import {
  Form,
  Input,
  FormContainer,
  FormHeading,
  InputGroup,
  Label,
} from "./styles";
import { H1, LinkSpan, Text } from "@/assets/css/global.styles";
import { Link } from "react-router-dom";
import { toast } from "react-toastify";

export default function ResetPasswordForm() {
  const [email, setEmail] = useState<string>();
  const [loading, setLoading] = useState<boolean>(false);

  async function handleSubmit(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    setLoading(true);
    http
      .post("/auth/password/reset", {
        email: email,
      })
      .then(() => {
        setLoading(false);
        toast.success("Um email com o link de confirmação foi enviado!");
      })
      .catch(() => {
        setLoading(false);
        toast.error("Oops, ocorreu um erro, tente novamente mais tarde!");
      });
  }

  return (
    <FormContainer>
      <FormHeading>
        <H1>Recuperar Senha</H1>
        <Text>
          Entre com seus dados para recuperar a senha e acessar o sistema
        </Text>
      </FormHeading>
      <Form onSubmit={handleSubmit}>
        <InputGroup>
          <Label>Email</Label>
          <Input
            type="email"
            name="email"
            onChange={(e) => setEmail(e.target.value)}
          />
        </InputGroup>
        <FilledButton type="submit" loading={loading ? 1 : 0}>
          Recuperar
        </FilledButton>
        <Text>
          Lembrou sua senha?{" "}
          <Link to="/login" style={{ textDecoration: "none" }}>
            <LinkSpan>Login</LinkSpan>
          </Link>
        </Text>
      </Form>
    </FormContainer>
  );
}
