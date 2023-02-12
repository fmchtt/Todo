import User from "../../types/user";
import http from "../http";

export async function getActualUser() {
  const { data } = await http.get<User>("/auth/me");

  return data;
}
