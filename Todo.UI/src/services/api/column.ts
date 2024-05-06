import { CreateColumn, EditColumn, ExpandedColumn } from "@/types/column";
import http from "../http";
import { MessageResponse } from "@/types/responses/message";

export async function createColumn(reqData: CreateColumn) {
  const { data } = await http.post<ExpandedColumn>(`/columns`, reqData);
  return data;
}

export async function editColumn(reqData: EditColumn) {
  const { data } = await http.patch<ExpandedColumn>(
    `/columns/${reqData.id}`,
    reqData,
  );
  return data;
}

export async function deleteColumn(id: string) {
  const { data } = await http.delete<MessageResponse>(`/columns/${id}`);
  return data;
}
