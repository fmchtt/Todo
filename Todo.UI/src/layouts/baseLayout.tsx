import { Outlet } from "react-router-dom";
import AuthGuard from "@/components/authGuard";
import Header from "@/components/header";
import SideBar from "@/components/sidebar";
import { Container, MainContent } from "./styles";
import { Suspense } from "react";
import BaseLoader from "./baseLoader";

export default function BaseLayout() {
  return (
    <AuthGuard>
      <Header />
      <Container>
        <SideBar />
        <MainContent>
          <Suspense fallback={<BaseLoader />}>
            <Outlet />
          </Suspense>
        </MainContent>
      </Container>
    </AuthGuard>
  );
}
