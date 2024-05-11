import http from "./http";
import { PageResult } from "@/types/responses/page";
import {
  CreateBoard,
  EditBoard,
  ExpandedBoard,
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
}

export default new BoardService();
