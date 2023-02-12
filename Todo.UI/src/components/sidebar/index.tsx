import { Link } from "react-router-dom";
import { ButtonGroup, SideBarButton, StyledSideBar } from "./styles";
import { HiOutlineHome } from "react-icons/hi";
import { FaTasks } from "react-icons/fa";
import { BsCalendar2Range } from "react-icons/bs";
import { IoExitOutline } from "react-icons/io5";
import useAuth from "../../context/auth";

export default function SideBar() {
  const { logout } = useAuth();

  return (
    <StyledSideBar>
      <ButtonGroup>
        <SideBarButton as={Link} to="/home">
          <HiOutlineHome size={30} /> Dashboard
        </SideBarButton>
        <SideBarButton as={Link} to="/home">
          <BsCalendar2Range size={26} /> Quadros
        </SideBarButton>
        <SideBarButton as={Link} to="/home">
          <FaTasks size={26} /> Tarefas
        </SideBarButton>
      </ButtonGroup>
      <SideBarButton onClick={logout}>
        <IoExitOutline size={26} /> Sair
      </SideBarButton>
    </StyledSideBar>
  );
}
