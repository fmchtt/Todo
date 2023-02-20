import { useContext, createContext, useState, useEffect } from "react";
import http from "../services/http";
import User from "../types/user";
import { TokenResponse } from "../types/responses/auth";
import { useQuery, useQueryClient } from "react-query";
import { getActualUser } from "../services/api/user";
import { LoginProps, RegisterProps, ContextProps, Context } from "./types";

const authContext = createContext({} as Context);

export function AuthProvider({ children }: ContextProps) {
  const [token, setToken] = useState<string | null>(
    localStorage.getItem("token")
  );

  useEffect(() => {
    if (token) {
      http.defaults.headers.common["Authorization"] = `Bearer ${token}`;
      client.invalidateQueries(["me"]);
    }
  }, [token]);

  const { data } = useQuery<User>(["me"], getActualUser, {
    onError: () => {
      setToken(null);
    },
  });
  const client = useQueryClient();

  http.interceptors.response.use(
    (success) => {
      return success;
    },
    (error) => {
      if (error.response.status === 401 || error.response.status === 403) {
        setToken(null);
        localStorage.removeItem("token");
        client.invalidateQueries(["me"]);
      }

      return Promise.reject(error);
    }
  );

  async function login(formData: LoginProps) {
    const { data } = await http.post<TokenResponse>("auth/login", formData);
    setToken(data.token);

    localStorage.setItem("token", data.token);
  }

  async function register(formData: RegisterProps) {
    const { data } = await http.post<TokenResponse>("auth/register", formData);
    setToken(data.token);

    localStorage.setItem("token", data.token);
  }

  function logout() {
    localStorage.removeItem("token");
    setToken(null);
    client.setQueryData("me", null);
  }

  return (
    <authContext.Provider
      value={{
        user: data,
        login,
        register,
        logout,
      }}
    >
      {children}
    </authContext.Provider>
  );
}

export default function useAuth() {
  return useContext(authContext);
}
