import http from "./http";
import { PageResult } from "@/types/responses/page";
import {
  CreateBoard,
  EditBoard,
  ExpandedBoard,
  ParticipantInvite,
  ParticipantRemove,
  ResumedBoard,
} from "@/types/board";
import { MessageResponse } from "@/types/responses/message";

export class BoardService {
  async getBoards() {
    const { data } = await http.get<PageResult<ResumedBoard>>("/boards");
    return data.results;
  }

  async getBoardById(id: string) {
    const { data } = await http.get<ExpandedBoard>(`/boards/${id}`);
    return data;
  }

  async createBoard(values: CreateBoard) {
    const { data } = await http.post<ResumedBoard>("/boards", values);
    return data;
  }

  async updateBoard({ id, ...values }: EditBoard) {
    const { data } = await http.patch<ResumedBoard>(`/boards/${id}`, values);
    return data;
  }

  async deleteBoard(id: string) {
    const { data } = await http.delete<MessageResponse>(`/boards/${id}`);
    return data;
  }

  async createBoardParticipant(boardId: string) {
    const { data } = await http.get<MessageResponse>(
      `/boards/${boardId}/invite/confirm`
    );

    return data;
  }

  async sendBoardInvite({ emails, boardId }: ParticipantInvite) {
    const { data } = await http.post<MessageResponse>(
      `/boards/${boardId}/invite`,
      { emails }
    );

    return data;
  }

  async deleteBoardParticipant({ boardId, participantId }: ParticipantRemove) {
    const { data } = await http.delete<MessageResponse>(
      `/boards/${boardId}/participant/${participantId}`
    );
    return data;
  }
}

export default new BoardService();
