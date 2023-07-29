import { FormEvent, ChangeEvent, useState, useRef } from "react";
import FilledButton from "../filledButton";
import {
  Form,
  ImagePreview,
  Input,
  InputGroup,
  Label,
  HoverImage,
} from "./styles";
import { getActualUser, patchUser } from "@/services/api/user";
import { useQuery, useQueryClient } from "@tanstack/react-query";
import { TbEdit } from "react-icons/tb";

export default function ProfileForm() {
  const client = useQueryClient();
  const fileRef = useRef<HTMLInputElement>(null);
  const meQuery = useQuery({ queryKey: ["me"], queryFn: getActualUser });
  const [imageBase64, setImageBase64] = useState<string>();
  const [hover, setHover] = useState<boolean>(false);

  function handleSubmit(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    const formData = new FormData(e.target as HTMLFormElement);

    patchUser(formData).then((res) => {
      client.setQueryData(["me"], res);
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
        <HoverImage
          hover={hover}
          onMouseEnter={() => setHover(true)}
          onMouseLeave={() => setHover(false)}
          onClick={() => {
            fileRef.current?.click();
          }}
        >
          <TbEdit style={{ color: "#fff", fontSize: "40px" }} />
        </HoverImage>
        <ImagePreview
          src={
            imageBase64 ||
            import.meta.env.VITE_API_URL + meQuery.data?.avatarUrl
          }
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
