import { Link } from "react-router-dom";
import {
  ButtonGroup,
  SideBarButton,
  StyledSideBar,
  TitleOption,
} from "./styles";
import useAuth from "@/context/auth";
import { ReactNode, useState } from "react";
import {
  TbHome,
  TbLayoutKanban,
  TbList,
  TbLogout,
  TbMenu2,
} from "react-icons/tb";

interface Options {
  icon: ReactNode;
  text: string;
  route?: string;
  action?: () => void;
}

export default function SideBar() {
  const [showOptions, setShowOptions] = useState(false);
  const { logout } = useAuth();

  const options: Options[] = [
    {
      icon: <TbHome size={22} />,
      text: "Dashboard",
      route: "/home",
    },
    {
      icon: <TbLayoutKanban size={22} />,
      text: "Quadros",
      route: "/boards",
    },
    {
      icon: <TbList size={22} />,
      text: "Tarefas",
      route: "/tasks",
    },
  ];

  return (
    <StyledSideBar>
      <ButtonGroup>
        <TbMenu2
          className="icon-menu"
          onClick={() => {
            setShowOptions((prev) => !prev);
          }}
        />
        {options.map((option, index) => {
          return option.route ? (
            <SideBarButton
              $show={showOptions}
              key={index}
              as={Link}
              to={option.route}
            >
              {option.icon}
              <TitleOption $show={showOptions}>{option.text}</TitleOption>
            </SideBarButton>
          ) : (
            <SideBarButton
              $show={showOptions}
              key={index}
              onClick={option.action}
            >
              {option.icon}
              <TitleOption $show={showOptions}>{option.text}</TitleOption>
            </SideBarButton>
          );
        })}
      </ButtonGroup>
      <SideBarButton onClick={logout}>
        <TbLogout size={22} />
        <TitleOption $show={showOptions}>Sair</TitleOption>
      </SideBarButton>
    </StyledSideBar>
  );
}
