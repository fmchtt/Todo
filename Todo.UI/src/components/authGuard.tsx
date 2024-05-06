import { ReactNode } from "react";
import { Navigate } from "react-router-dom";
import useAuth from "@/context/auth";
import BaseLoader from "@/layouts/baseLoader";
import Header from "./header";

export default function AuthGuard({ children }: { children: ReactNode }) {
  const { user, isLoading } = useAuth();

  if (!user && isLoading) {
    return (
      <>
        <Header />
        <BaseLoader />
      </>
    );
  }

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
