import { MessageResponse } from "@/types/responses/message";
import http from "./http";
import {
  CreateColumn,
  DeleteColumn,
  EditColumn,
  ResumedColumn,
} from "@/types/column";

class ColumnService {
  async createColumn(values: CreateColumn) {
    const { data } = await http.post<ResumedColumn>(`/columns`, values);
    return data;
  }

  async updateColumn({ id, ...values }: EditColumn) {
    const { data } = await http.patch<ResumedColumn>(`/columns/${id}`, values);
    return data;
  }

  async deleteColumn({ id }: DeleteColumn) {
    const { data } = await http.delete<MessageResponse>(`/columns/${id}`);
    return data;
  }
}

export default new ColumnService();
