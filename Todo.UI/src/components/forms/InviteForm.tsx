import User from "@/types/user";
import { useState, FormEvent } from "react";
import { Description, Form, Group, Input, InputGroup, Label } from "./styles";
import FilledButton from "../filledButton";
import { Text } from "@/assets/css/global.styles";
import { TbTrash } from "react-icons/tb";
import { removeParticipant, sendInvite } from "@/services/api/boards";
import useAuth from "@/context/auth";
import RoundedAvatar from "@/components/roundedAvatar";

interface InviteFormProps {
  participants?: User[];
  ownerId?: string;
  boardId: string;
}

export default function InviteForm(props: InviteFormProps) {
  const [emails, setEmails] = useState("");
  const [loading, setLoading] = useState(false);
  const { user } = useAuth();

  async function handleSubmit(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();

    setLoading(true);
    const emailList = emails.split(",");

    try {
      await sendInvite(emailList, props.boardId);
      setEmails("");
    } catch (e) {
      console.log(e);
    }

    setLoading(false);
  }

  async function handleRemoveParticipant(participantId: string) {
    setLoading(true);

    try {
      await removeParticipant(participantId, props.boardId);
    } catch (e) {
      console.log(e);
    }

    setLoading(false);
  }

  return (
    <Form onSubmit={handleSubmit}>
      <InputGroup row gap={10}>
        <InputGroup>
          <Label>Email</Label>
          <Input
            type="text"
            value={emails}
            onChange={(e) => setEmails(e.target.value)}
          />
          <Description>
            Emails separados por virgula. ex: email@email,email2@email.com
          </Description>
        </InputGroup>
        <FilledButton loading={loading ? 1 : 0}>Convidar</FilledButton>
      </InputGroup>
      <Text>Participantes: </Text>
      {props.participants?.map((participant) => {
        return (
          <Group justify="space-between" align="center" key={participant.id}>
            <Group align="center">
              <RoundedAvatar
                avatarUrl={
                  participant.avatarUrl &&
                  import.meta.env.VITE_API_URL + participant.avatarUrl
                }
                name={participant.name}
              />
              <Text weight={200}>{participant.name}</Text>
            </Group>
            <Group>
              {participant.id !== user?.id &&
                participant.id !== props.ownerId && (
                  <TbTrash
                    role="button"
                    size={22}
                    cursor="pointer"
                    onClick={() => {
                      handleRemoveParticipant(participant.id);
                    }}
                  />
                )}
            </Group>
          </Group>
        );
      })}
    </Form>
  );
}
