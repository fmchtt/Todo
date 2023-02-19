import { ExpandedBoard, ResumedBoard } from "../../types/board";
import http from "../http";

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

export async function getBoardById(id: string) {
  const { data } = await http.get<ExpandedBoard>(`/boards/${id}`);
  return data;
}

interface UpdateBoardData {
  id: string;
  name: string;
}
export async function patchBoardById(reqData: UpdateBoardData) {
  const { data } = await http.patch<ResumedBoard>(
    `/boards/${reqData.id}`,
    reqData
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
