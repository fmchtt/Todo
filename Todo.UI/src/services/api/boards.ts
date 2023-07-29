import { UseQueryOptions } from "@tanstack/react-query";
import { ExpandedBoard, ResumedBoard } from "@/types/board";
import http from "../http";
import { MessageResponse } from "@/types/responses/message";
import { PageResult } from "@/types/responses/page";

export async function getBoards() {
  const { data } = await http.get<PageResult<ResumedBoard>>("/boards");
  return data.results;
}

interface CreateBoardData {
  name: string;
}
export async function postBoard(reqData: CreateBoardData) {
  const { data } = await http.post<ResumedBoard>("/boards", reqData);
  return data;
}

export async function getBoardById(query: UseQueryOptions) {
  const id = query.queryKey?.at(1);
  if (!id) {
    return undefined;
  }

  const { data } = await http.get<ExpandedBoard>(`/boards/${id}`);
  return data;
}

interface UpdateBoardData {
  id: string;
  values: { name: string; description: string };
}
export async function patchBoard(reqData: UpdateBoardData) {
  const { data } = await http.patch<ResumedBoard>(
    `/boards/${reqData.id}`,
    reqData.values
  );
  return data;
}

export async function deleteBoardById(id: string) {
  const { data } = await http.delete<MessageResponse>(`/boards/${id}`);
  return data;
}

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
