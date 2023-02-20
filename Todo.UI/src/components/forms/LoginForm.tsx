import { FormContainer, Input } from "./styles";
import { InputGroup, Label, Form } from "./styles";
import { H1, Text, A } from "../../assets/css/global.styles";
import useAuth from "../../context/auth";
import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import FilledButton from "../filledButton";
import { useFormik } from "formik";
import * as Yup from "yup";
import ErrorMessage from "./ErrorMessage";
import { TbEye, TbEyeOff } from "react-icons/tb";

export default function LoginForm() {
  const { user, login } = useAuth();
  const [showPassword, setShowPassword] = useState<string>("password");
  const [loading, setLoading] = useState<boolean>(false);
  const navigate = useNavigate();

  useEffect(() => {
    if (user) {
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
      email: "",
      password: "",
    },
    onSubmit: (values) => {
      setLoading(true);
      login({
        email: values.email,
        password: values.password,
      }).then(() => {
        setLoading(false);
      });
    },

    validationSchema: Yup.object({
      email: Yup.string().required("Este campo é obrigatório"),
      password: Yup.string().required("Este campo é obrigatório"),
    }),
    validateOnMount: false,
    validateOnBlur: false,
    validateOnChange: false,
  });

  return (
    <FormContainer>
      <H1>Login</H1>
      <Text>Faça login para acessar o app</Text>
      <Form onSubmit={formik.handleSubmit}>
        <InputGroup>
          <Label>Email</Label>
          <Input
            type="email"
            value={formik.values.email}
            onChange={formik.handleChange}
            name="email"
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
            onChange={formik.handleChange}
            name="password"
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
        <FilledButton size="25px" loading={loading ? 1 : 0}>
          Entrar
        </FilledButton>
        <Text>
          Ainda não tem conta?{" "}
          <Link to={"/register"} style={{ textDecoration: "none" }}>
            <A>Registre-se</A>
          </Link>
        </Text>
      </Form>
    </FormContainer>
  );
}
