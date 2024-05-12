import { User } from "@/types/user";
import { Group } from "./styles";
import { Text } from "@/assets/css/global.styles";
import { TbTrash } from "react-icons/tb";
import RoundedAvatar from "@/components/roundedAvatar";
import { toast } from "react-toastify";
import Form from "../form";
import {
  useBoardParticipantAdd,
  useBoardParticipantRemove,
} from "@/adapters/boardAdapters";
import { useRef } from "react";
import { FormikProps } from "formik";

interface InviteFormProps {
  participants?: User[];
  ownerId?: string;
  boardId: string;
}

export default function InviteForm(props: InviteFormProps) {
  const formRef = useRef<FormikProps<{ emails: string }> | null>();

  const sendInviteMutation = useBoardParticipantAdd({
    onSuccess: () => {
      toast.success("Convite enviado com sucesso!");
      formRef.current?.resetForm({
        values: {
          emails: "",
        },
      });
    },
    onError: () => {
      toast.error(
        "Oops, ocorreu um erro ao enviar convite, tente novamente mais tarde!"
      );
    },
  });

  const removeParticipantMutation = useBoardParticipantRemove({
    onSuccess: () => {
      toast.success("Participante removido com sucesso!");
    },
    onError: () => {
      toast.error(
        "Oops, ocorreu um erro ao remover participante, tente novamente mais tarde!"
      );
    },
  });

  async function handleSubmit(values: { emails: string }) {
    sendInviteMutation.mutate({
      boardId: props.boardId,
      emails: values.emails.split(","),
    });
  }

  async function handleRemoveParticipant(participantId: string) {
    removeParticipantMutation.mutate({
      participantId: participantId,
      boardId: props.boardId,
    });
  }

  return (
    <>
      <Form
        initialValues={{ emails: "" }}
        onSubmit={handleSubmit}
        innerRef={(ref) => (formRef.current = ref)}
      >
        <Form.Input
          label="Email's"
          name="emails"
          description="Emails separados por virgula. ex: email1@email.com, email2@email.com"
        />
        <Form.Submit
          label="Convidar"
          $margin="2px"
          $loading={
            removeParticipantMutation.isPending || sendInviteMutation.isPending
          }
        />
      </Form>
      <Text>Participantes: </Text>
      <Group $col $gap={5}>
        {props.participants?.map((participant) => {
          return (
            <Group
              $justify="space-between"
              $align="center"
              key={participant.id}
            >
              <Group $align="center">
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
                {participant.id !== props.ownerId && (
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
      </Group>
    </>
  );
}
