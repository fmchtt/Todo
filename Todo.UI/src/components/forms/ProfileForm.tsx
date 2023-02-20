import { FormEvent } from "react";
import FilledButton from "../filledButton";
import { Form, Input, InputGroup, Label } from "./styles";
import { getActualUser, patchUser } from "../../services/api/user";
import { useQuery, useQueryClient } from "react-query";

export default function ProfileForm() {
  const client = useQueryClient();
  const meQuery = useQuery(["me"], getActualUser);

  function handleSubmit(e: FormEvent<HTMLFormElement>) {
    const formData = new FormData(e.target as HTMLFormElement);

    patchUser(formData).then((res) => {
      client.setQueryData("me", res);
    });
  }

  return (
    <Form width="clamp(600px, 20%, 90%)" onSubmit={handleSubmit}>
      <InputGroup>
        <Label>Nome</Label>
        <Input defaultValue={meQuery.data?.name} />
      </InputGroup>
      <InputGroup>
        <Label>Foto de perfil</Label>
        <Input type="file" />
      </InputGroup>
      <FilledButton type="submit">Salvar</FilledButton>
    </Form>
  );
}
