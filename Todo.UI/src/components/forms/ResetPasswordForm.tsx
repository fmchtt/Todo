import http from "@/services/http";
import { useState } from "react";
import { FormContainer, FormHeading, FormFooter } from "./styles";
import { H1, LinkSpan, Text } from "@/assets/css/global.styles";
import { Link } from "react-router-dom";
import { toast } from "react-toastify";
import Form from "../form";

export default function ResetPasswordForm() {
  const [loading, setLoading] = useState<boolean>(false);

  async function handleSubmit(values: { email: string }) {
    setLoading(true);
    http
      .post("/auth/password/reset", {
        email: values.email,
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
      <Form initialValues={{ email: "" }} onSubmit={handleSubmit}>
        <Form.Input
          name="email"
          label="Email"
          placeholder="Ex: teste@email.com"
        />
        <Form.Submit label="Recuperar" $loading={loading} />
      </Form>
      <FormFooter>
        <Text>
          Lembrou sua senha?{" "}
          <Link to="/login" style={{ textDecoration: "none" }}>
            <LinkSpan>Login</LinkSpan>
          </Link>
        </Text>
      </FormFooter>
    </FormContainer>
  );
}
