import User from "@/types/user";
import { useState } from "react";
import { Group } from "./styles";
import { Text } from "@/assets/css/global.styles";
import { TbTrash } from "react-icons/tb";
import { removeParticipant, sendInvite } from "@/services/api/boards";
import useAuth from "@/context/auth";
import RoundedAvatar from "@/components/roundedAvatar";
import { toast } from "react-toastify";
import Form from "../form";

interface InviteFormProps {
  participants?: User[];
  ownerId?: string;
  boardId: string;
}

export default function InviteForm(props: InviteFormProps) {
  const [loading, setLoading] = useState(false);
  const { user } = useAuth();

  async function handleSubmit(values: { emails: string }) {
    setLoading(true);
    const emailList = values.emails.split(",");

    try {
      await sendInvite(emailList, props.boardId);

      toast.success("Convite enviado com sucesso!");
    } catch (e) {
      toast.error("Oops, ocorreu um erro, tente novamente mais tarde!");
    }

    setLoading(false);
  }

  async function handleRemoveParticipant(participantId: string) {
    setLoading(true);

    try {
      await removeParticipant(participantId, props.boardId);

      toast.success("Participante removido com sucesso!");
    } catch (e) {
      toast.error("Oops, ocorreu um erro, tente novamente mais tarde!");
    }

    setLoading(false);
  }

  return (
    <>
      <Form initialValues={{ emails: "" }} onSubmit={handleSubmit}>
        <Form.Input
          label="Email's"
          name="emails"
          description="Emails separados por virgula. ex: email1@email.com, email2@email.com"
        />
        <Form.Submit label="Convidar" $loading={loading} />
      </Form>
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
              <Text $weight={200}>{participant.name}</Text>
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
    </>
  );
}
