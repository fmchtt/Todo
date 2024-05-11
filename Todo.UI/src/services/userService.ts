import { User } from "@/types/user";
import http from "./http";
import { LoginProps, RegisterProps } from "@/context/types";
import { TokenResponse } from "@/types/responses/auth";
import storageService, { IStorageService } from "./storageService";

export class UserService {
  constructor(private storageService: IStorageService) {}

  async getActualUser() {
    const { data } = await http.get<User>("/auth/me");
    return data;
  }

  async patchUser(reqData: FormData) {
    const { data } = await http.patch<User>("/users", reqData);
    return data;
  }

  async login(values: LoginProps) {
    const { data } = await http.post<TokenResponse>("auth/login", values);

    this.storageService.save("token", data.token);

    return data;
  }

  async register(values: RegisterProps) {
    const { data } = await http.post<TokenResponse>("auth/register", values);

    this.storageService.save("token", data.token);

    return data;
  }

  async logout() {
    this.storageService.remove("token");
  }
}

export default new UserService(storageService);
