import { PageResult } from "@/types/responses/page";
import http from "./http";
import { MessageResponse } from "@/types/responses/message";
import {
  Comment,
  CreateComment,
  DeleteComment,
  EditComment,
} from "@/types/comment";

class CommentService {
  async getCommentsByItemId(id: string) {
    const { data } = await http.get<PageResult<Comment>>(`comments/${id}`);
    return data.results;
  }

  async createComment({ itemId, text }: CreateComment) {
    const { data } = await http.post<Comment>(`comments/${itemId}`, {
      text: text,
    });
    return data;
  }

  async updateComment({ id, text }: EditComment) {
    const { data } = await http.patch<Comment>(`comments/${id}`, {
      text: text,
    });
    return data;
  }

  async deleteComment({ id }: DeleteComment) {
    const { data } = await http.delete<MessageResponse>(`comments/${id}`);
    return data;
  }
}

export default new CommentService();
