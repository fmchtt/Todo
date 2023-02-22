import { FormEvent, ChangeEvent, useState, useRef } from "react";
import FilledButton from "../filledButton";
import { Form, ImagePreview, Input, InputGroup, Label } from "./styles";
import { getActualUser, patchUser } from "@/services/api/user";
import { useQuery, useQueryClient } from "react-query";

export default function ProfileForm() {
  const client = useQueryClient();
  const fileRef = useRef<HTMLInputElement>(null);
  const meQuery = useQuery(["me"], getActualUser);
  const [imageBase64, setImageBase64] = useState<string>();

  function handleSubmit(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    const formData = new FormData(e.target as HTMLFormElement);

    patchUser(formData).then((res) => {
      client.setQueryData("me", res);
      setImageBase64(undefined);
    });
  }

  function handleImageChange(e: ChangeEvent<HTMLInputElement>) {
    if (!e.target.files || e.target.files.length < 1) {
      return;
    }

    const base64 = URL.createObjectURL(e.target.files[0]);
    setImageBase64(base64);
  }

  return (
    <Form width="clamp(600px, 20%, 90%)" onSubmit={handleSubmit}>
      <InputGroup centralized>
        <input
          ref={fileRef}
          name="file"
          type="file"
          hidden
          onChange={handleImageChange}
        />
        <ImagePreview
          src={
            imageBase64 ||
            import.meta.env.VITE_API_URL + meQuery.data?.avatarUrl
          }
          onClick={() => {
            fileRef.current?.click();
          }}
        />
      </InputGroup>
      <InputGroup>
        <Label>Nome</Label>
        <Input name="name" defaultValue={meQuery.data?.name} />
      </InputGroup>
      <FilledButton type="submit">Salvar</FilledButton>
    </Form>
  );
}
