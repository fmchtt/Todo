import { FormHeading, FormFooter } from "./styles";
import { LinkSpan, H1, Text } from "@/assets/css/global.styles";
import { FormContainer } from "./styles";
import { useEffect, useState } from "react";
import useAuth from "@/context/auth";
import { Link, useNavigate, useSearchParams } from "react-router-dom";
import * as Yup from "yup";
import Form from "../form";

export default function RegisterForm() {
  const [loading, setLoading] = useState<boolean>(false);
  const { user, register } = useAuth();
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

  const validationSchema = Yup.object({
    name: Yup.string().required("O nome é obrigatório!"),
    email: Yup.string().required("O email é obrigatório!"),
    password: Yup.string().required("A senha é obrigatória!"),
  });

  function handleSubmit(values: {
    name: string;
    email: string;
    password: string;
  }) {
    setLoading(true);
    register({
      name: values.name,
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

  return (
    <FormContainer>
      <FormHeading>
        <H1>Registre-se</H1>
        <Text>Para ter acesso ao app é necessário ter uma conta. Crie uma</Text>
      </FormHeading>
      <Form
        initialValues={{
          name: "",
          email: "",
          password: "",
        }}
        onSubmit={handleSubmit}
        validationSchema={validationSchema}
      >
        <Form.Input name="name" placeholder="Laura Silva" label="Nome" />
        <Form.Input
          name="email"
          placeholder="Ex: teste@email.com"
          label="Email"
        />
        <Form.Password
          label="Senha"
          name="password"
          placeholder="Ex: Senha1234@"
        />
        <Form.Submit label="Registrar" $loading={loading} />
      </Form>
      <FormFooter>
        <Text>
          Já tem conta? Faça{" "}
          <Link
            to={`/login${
              searchParams.get("next")
                ? "?next=" + searchParams.get("next")
                : ""
            }`}
            style={{ textDecoration: "none" }}
          >
            <LinkSpan>Login</LinkSpan>
          </Link>
        </Text>
      </FormFooter>
    </FormContainer>
  );
}
