import { UseQueryOptions } from "react-query";
import { ExpandedBoard, ResumedBoard } from "@/types/board";
import http from "../http";
import { MessageResponse } from "@/types/responses/message";

export async function getBoards() {
  const { data } = await http.get<ResumedBoard[]>("/boards");
  return data;
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
