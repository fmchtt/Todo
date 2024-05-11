import {
  useContext,
  createContext,
  useState,
  useEffect,
  ReactNode,
} from "react";
import http from "../services/http";
import { useQueryClient } from "@tanstack/react-query";
import { useUser } from "@/adapters/userAdapters";
import userService from "@/services/userService";
import { LoginProps, RegisterProps, User } from "@/types/user";

type Context = {
  user: User | undefined;
  isLoading: boolean;
  register: (formData: RegisterProps) => Promise<void>;
  login: (formData: LoginProps) => Promise<void>;
  logout: () => void;
};
const authContext = createContext({} as Context);

type ContextProps = {
  children: ReactNode;
};
export function AuthProvider({ children }: ContextProps) {
  const client = useQueryClient();
  const [token, setToken] = useState<string | null>(
    localStorage.getItem("token")
  );

  useEffect(() => {
    if (token) {
      http.defaults.headers.common["Authorization"] = `Bearer ${token}`;
      client.invalidateQueries({
        queryKey: ["me"],
      });
    }
  }, [token]);

  const { data, isLoading } = useUser({
    enabled: !!token,
  });

  http.interceptors.response.use(
    (success) => {
      return success;
    },
    (error) => {
      if (error.response.status === 401 || error.response.status === 403) {
        setToken(null);
        localStorage.removeItem("token");
        client.removeQueries();
      }

      return Promise.reject(error);
    }
  );

  async function login(formData: LoginProps) {
    const data = await userService.login(formData);
    setToken(data.token);
  }

  async function register(formData: RegisterProps) {
    const data = await userService.register(formData);
    setToken(data.token);
  }

  function logout() {
    userService.logout();
    setToken(null);
    client.removeQueries();
  }

  return (
    <authContext.Provider
      value={{
        user: data,
        isLoading,
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
