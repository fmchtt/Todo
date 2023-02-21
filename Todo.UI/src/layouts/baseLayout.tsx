import { Outlet } from "react-router-dom";
import AuthGuard from "@/components/authGuard";
import Header from "@/components/header";
import SideBar from "@/components/sidebar";
import { Container, MainContent } from "./styles";

export default function BaseLayout() {
  return (
    <AuthGuard>
      <Header />
      <Container>
        <SideBar />
        <MainContent>
          <Outlet />
        </MainContent>
      </Container>
    </AuthGuard>
  );
}
