import { useContext, createContext, ReactNode, useState } from "react";
import { useQueryClient } from "react-query";
import http from "../services/http";

interface ContextProps {
  children: ReactNode;
}

interface ContextLogin {
  email: string;
  password: string;
}

interface ContextSignUp {
  name: string;
  email: string;
  password: string;
}

const initialState = {};

const authContext = createContext({
  state: initialState,
});

export function AuthProvider({ children }: ContextProps) {
  const [authed, setAuthed] = useState<boolean>();
  const [user, setUser] = useState<object>();

  function login({ email, password }: ContextLogin) {
    return new Promise(async (resolve, reject) => {
      const formData = new FormData();
      formData.append("email", email);
      formData.append("password", password);
      http
        .post("login", formData)
        .then((res) => {
          setUser(res.data);
          setAuthed(true);
          return resolve;
        })
        .catch((e) => {
          setUser({});
          setAuthed(false);
          return reject(e.response.data);
        });
    });
  }

  function signUp(reqData: ContextSignUp) {
    return new Promise(async (resolve, reject) => {
      try {
        const { data } = await http.post("register", reqData);
        setUser(data);
        setAuthed(true);
        return resolve(data);
      } catch (e: any) {
        return reject(e.response.data);
      }
    });
  }

  return (
    <authContext.Provider
      value={{
        state: {
          user,
          authed,
          login,
          signUp,
        },
      }}
    >
      {children}
    </authContext.Provider>
  );
}

export default function useAuth() {
  return useContext(authContext);
}
