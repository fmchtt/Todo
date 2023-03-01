import { ReactNode } from "react";
import User from "@/types/user";

export type LoginProps = {
  email: string;
  password: string;
};

export type RegisterProps = LoginProps & {
  name: string;
};

export interface ContextProps {
  children: ReactNode;
}

export interface Context {
  user: User | undefined;
  isLoading: boolean;
  register: (formData: RegisterProps) => Promise<void>;
  login: (formData: LoginProps) => Promise<void>;
  logout: () => void;
}
