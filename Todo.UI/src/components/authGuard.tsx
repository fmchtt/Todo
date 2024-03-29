import { ReactNode } from "react";
import { Navigate } from "react-router-dom";
import useAuth from "@/context/auth";

export default function AuthGuard({ children }: { children: ReactNode }) {
  const { user } = useAuth();

  if (!user) {
    return (
      <Navigate
        to={`/login${
          window.location.pathname !== "/login"
            ? `?next=${window.location.pathname}`
            : ""
        }`}
      />
    );
  }

  return <>{children}</>;
}
