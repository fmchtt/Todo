import { FormContainer, FormFooter, FormHeading } from "./styles";
import { H1, Text, LinkSpan } from "@/assets/css/global.styles";
import useAuth from "@/context/auth";
import { useEffect, useState } from "react";
import { Link, useNavigate, useSearchParams } from "react-router-dom";
import * as Yup from "yup";
import Form from "../form";

export default function LoginForm() {
  const { user, login } = useAuth();
  const [loading, setLoading] = useState<boolean>(false);
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();

  useEffect(() => {
    if (user) {
      const next = searchParams.get("next");
      if (next && next !== "/login") {
        return navigate(next);
      }

      navigate("/home");
    }
  }, [user]);

  function handleSubmit(values: { email: string; password: string }) {
    setLoading(true);
    login({
      email: values.email,
      password: values.password,
    })
      .then(() => {
        setLoading(false);
      })
      .catch(() => {
        setLoading(false);
      });
  }

  const validationSchema = Yup.object({
    email: Yup.string().required("O email é obrigatório"),
    password: Yup.string().required("A senha é obrigatório"),
  });

  return (
    <FormContainer>
      <FormHeading>
        <H1>Login</H1>
        <Text>Faça login para acessar o app</Text>
      </FormHeading>
      <Form
        initialValues={{
          email: "",
          password: "",
        }}
        onSubmit={handleSubmit}
        validationSchema={validationSchema}
      >
        <Form.Input
          label="Email"
          name="email"
          placeholder="Ex: teste@email.com"
        />
        <Form.Password
          label="Senha"
          name="password"
          placeholder="Ex: Senha1234@"
        />
        <Form.Submit label="Entrar" $loading={loading} />
      </Form>
      <FormFooter>
        <Text>
          Ainda não tem conta?{" "}
          <Link
            to={`/register${
              searchParams.get("next")
                ? "?next=" + searchParams.get("next")
                : ""
            }`}
            style={{ textDecoration: "none" }}
          >
            <LinkSpan>Registre-se</LinkSpan>
          </Link>
        </Text>
        <Text>
          Esqueceu sua senha ?{" "}
          <Link to="/password/reset" style={{ textDecoration: "none" }}>
            <LinkSpan>Recuperar</LinkSpan>
          </Link>
        </Text>
      </FormFooter>
    </FormContainer>
  );
}
