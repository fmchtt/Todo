import User from "@/types/user";
import http from "../http";

export async function getActualUser() {
  const { data } = await http.get<User>("/auth/me");
  return data;
}

export async function patchUser(reqData: FormData) {
  const { data } = await http.patch<User>("/users", reqData);
  return data;
}
