import http from "../http";
import { MessageResponse } from "@/types/responses/message";

export async function deleteBoardParticipantById(
  boardId: string,
  participantId: string
) {
  const { data } = await http.delete<MessageResponse>(
    `/boards/${boardId}/participant/${participantId}`
  );
  return data;
}

export async function createParticipant(boardId: string) {
  const { data } = await http.get<MessageResponse>(
    `/boards/${boardId}/invite/confirm`
  );

  return data;
}

export async function sendInvite(emails: string[], boardId: string) {
  const { data } = await http.post<MessageResponse>(
    `/boards/${boardId}/invite`,
    { emails }
  );

  return data;
}

export async function removeParticipant(
  participantId: string,
  boardId: string
) {
  const { data } = await http.delete<MessageResponse>(
    `/boards/${boardId}/participant/${participantId}`
  );
  return data;
}
