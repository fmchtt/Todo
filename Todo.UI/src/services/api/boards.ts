import http from "../http";

interface ReqBoard {
  id?: string;
  name?: string;
  participantId?: string;
}

export async function getBoards() {
  const { data } = await http.get("/boards");
  return data;
}

export async function postBoard(reqData: ReqBoard) {
  const { data } = await http.post("/boards", reqData);
  return data;
}

export async function getBoardById(reqData: ReqBoard) {
  const { data } = await http.get(`/boards/${reqData.id}`);
  return data;
}

export async function patchBoardById(reqData: ReqBoard) {
  const { data } = await http.patch(`/boards/${reqData.id}`);
  return data;
}

export async function deleteBoardById(reqData: ReqBoard) {
  const { data } = await http.delete(`/boards/${reqData.id}`);
  return data;
}

export async function deleteBoardParticipantById(reqData: ReqBoard) {
  const { data } = await http.delete(
    `/boards/${reqData.id}/participant/${reqData.participantId}`
  );
  return data;
}
