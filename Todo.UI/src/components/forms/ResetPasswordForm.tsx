import http from "@/services/http";
import { FormEvent, useState } from "react";
import FilledButton from "../filledButton";
import { Form, Description, Input, FormStyled } from "./styles";

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
      })
      .catch((e) => {
        console.log(e);
      });
  }

  return (
    <FormStyled>
      <Form onSubmit={handleSubmit} width="300px">
        <Description>Digite o email para recuperação de senha</Description>
        <Input
          type="email"
          name="email"
          onChange={(e) => setEmail(e.target.value)}
        />
        <FilledButton type="submit" loading={loading ? 1 : 0}>
          Enviar
        </FilledButton>
      </Form>
    </FormStyled>
  );
}
