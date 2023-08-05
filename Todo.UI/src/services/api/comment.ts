import http from "@/services/http";
import { PageResult } from "@/types/responses/page";
import { Comment } from "@/types/comment";
import { MessageResponse } from "@/types/responses/message";

export async function getCommentsByItemId(id: string) {
  const { data } = await http.get<PageResult<Comment>>(`comments/${id}`);
  return data;
}

export async function createComment(id: string, text: string) {
  const { data } = await http.post<Comment>(`comments/${id}`, { text: text });
  return data;
}

export async function editComment(id: string, text: string) {
  const { data } = await http.patch<Comment>(`comments/${id}`, { text: text });
  return data;
}

export async function deleteComment(id: string) {
  const { data } = await http.delete<MessageResponse>(`comments/${id}`);
  return data;
}
