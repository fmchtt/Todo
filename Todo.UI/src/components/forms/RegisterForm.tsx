import { InputGroup, Label, Form, Input, FormHeading } from "./styles";
import { LinkSpan, H1, Text } from "@/assets/css/global.styles";
import { FormContainer } from "./styles";
import FilledButton from "../filledButton";
import { useEffect, useState } from "react";
import useAuth from "@/context/auth";
import { Link, useNavigate, useSearchParams } from "react-router-dom";
import { useFormik } from "formik";
import * as Yup from "yup";
import ErrorMessage from "./ErrorMessage";
import { TbEye, TbEyeOff } from "react-icons/tb";
import { AxiosError } from "axios";

export default function RegisterForm() {
  const [loading, setLoading] = useState<boolean>(false);
  const [showPassword, setShowPassword] = useState<string>("password");
  const { user, register } = useAuth();
  const [error, setError] = useState<boolean>(false);
  const [message, setMessage] = useState<string>("");
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

  function eyeInput() {
    if (showPassword === "password") {
      setShowPassword("text");
    } else {
      setShowPassword("password");
    }
  }

  const formik = useFormik({
    initialValues: {
      name: "",
      email: "",
      password: "",
    },
    onSubmit: (values) => {
      setLoading(true);
      register({
        name: values.name,
        email: values.email,
        password: values.password,
      })
        .then(() => {
          setLoading(false);
        })
        .catch((e) => {
          setLoading(false);
          setError(true);
          if (e instanceof AxiosError) {
            setMessage(e.response?.data.message);
          }
        });
    },

    validationSchema: Yup.object({
      name: Yup.string().required("O nome é obrigatório!"),
      email: Yup.string().required("O email é obrigatório!"),
      password: Yup.string().required("A senha é obrigatória!"),
    }),
    validateOnMount: false,
    validateOnBlur: false,
    validateOnChange: false,
  });

  return (
    <FormContainer>
      <FormHeading>
        <H1>Registre-se</H1>
        <Text>Para ter acesso ao app é necessário ter uma conta. Crie uma</Text>
      </FormHeading>
      <Form onSubmit={formik.handleSubmit}>
        <InputGroup>
          <Label>Nome</Label>
          <Input
            type="text"
            name="name"
            value={formik.values.name}
            onChange={formik.handleChange}
            placeholder="Laura Silva"
          />
          {formik.errors.name && (
            <ErrorMessage>{formik.errors.name}</ErrorMessage>
          )}
        </InputGroup>
        <InputGroup>
          <Label>Email</Label>
          <Input
            type="email"
            name="email"
            value={formik.values.email}
            onChange={formik.handleChange}
            placeholder="Ex: teste@email.com"
          />
          {formik.errors.email && (
            <ErrorMessage>{formik.errors.email}</ErrorMessage>
          )}
        </InputGroup>
        <InputGroup>
          <Label>Senha</Label>
          <Input
            type={showPassword}
            value={formik.values.password}
            name="password"
            onChange={formik.handleChange}
            placeholder="Ex: Senha1234@"
          />
          {showPassword === "password" ? (
            <TbEye className="eye" onClick={eyeInput} />
          ) : (
            <TbEyeOff onClick={eyeInput} className="eye" />
          )}
          {formik.errors.password && (
            <ErrorMessage>{formik.errors.password}</ErrorMessage>
          )}
        </InputGroup>
        {error && <ErrorMessage>{message}</ErrorMessage>}
        <FilledButton $size="25px" type="submit" $loading={loading}>
          Registrar
        </FilledButton>
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
      </Form>
    </FormContainer>
  );
}
