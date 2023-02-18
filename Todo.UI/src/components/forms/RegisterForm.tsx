import { InputGroup, Label, Form } from "./styles";
import { A, H1, Text } from "../../assets/css/global.styles";
import { FormContainer } from "./styles";
import Input from "../input";
import FilledButton from "../filledButton";
import { useEffect, useState } from "react";
import useAuth from "../../context/auth";
import { Link, useNavigate } from "react-router-dom";
import { AiFillEye, AiFillEyeInvisible } from "react-icons/ai";
import { useFormik } from "formik";
import * as Yup from "yup";
import MessageErrorInput from "../messageErrorInput";

export default function RegisterForm() {
  const [loading, setLoading] = useState<boolean>(false);
  const [showPassword, setShowPassword] = useState<string>("password");
  const { user, register } = useAuth();
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
      }).then(() => {
        setLoading(false);
      });
    },

    validationSchema: Yup.object({
      name: Yup.string().required("Este campo é obrigatório"),
      email: Yup.string().required("Este campo é obrigatório"),
      password: Yup.string().required("Este campo é obrigatório"),
    }),
    validateOnMount: false,
    validateOnBlur: false,
    validateOnChange: false,
  });

  return (
    <FormContainer>
      <H1>Registre-se</H1>
      <Text>Para ter acesso ao app é necessário ter uma conta. Crie uma</Text>
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
            <MessageErrorInput>{formik.errors.name}</MessageErrorInput>
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
            <MessageErrorInput>{formik.errors.email}</MessageErrorInput>
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
            <AiFillEye className="eye" onClick={eyeInput} />
          ) : (
            <AiFillEyeInvisible onClick={eyeInput} className="eye" />
          )}
          {formik.errors.password && (
            <MessageErrorInput>{formik.errors.password}</MessageErrorInput>
          )}
        </InputGroup>
        <FilledButton size="25px" type="submit" loading={loading}>
          Registrar
        </FilledButton>
        <Text>
          Já tem conta? Faça{" "}
          <Link to={"/login"} style={{ textDecoration: "none" }}>
            <A>Login</A>
          </Link>
        </Text>
      </Form>
    </FormContainer>
  );
}
