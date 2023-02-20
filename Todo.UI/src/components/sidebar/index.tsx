import { Link } from "react-router-dom";
import {
  ButtonGroup,
  SideBarButton,
  StyledSideBar,
  TitleOption,
} from "./styles";
import useAuth from "../../context/auth";
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
  const [showOptions, setShowOptions] = useState<number>(0);
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
      route: "/home",
    },
    {
      icon: <TbList size={22} />,
      text: "Tarefas",
      route: "/home",
    },
  ];

  return (
    <StyledSideBar>
      <ButtonGroup>
        <TbMenu2
          className="icon-menu"
          onClick={() => {
            setShowOptions(showOptions === 1 ? 0 : 1);
          }}
        />
        {options.map((option, index) => {
          return option.action ? (
            <SideBarButton
              show={showOptions ? 1 : 0}
              key={index}
              onClick={option.action}
            >
              {option.icon}
              <TitleOption show={showOptions ? 1 : 0}>
                {option.text}
              </TitleOption>
            </SideBarButton>
          ) : (
            <SideBarButton
              show={showOptions ? 1 : 0}
              key={index}
              as={Link}
              to="/home"
            >
              {option.icon}
              <TitleOption show={showOptions ? 1 : 0}>
                {option.text}
              </TitleOption>
            </SideBarButton>
          );
        })}
      </ButtonGroup>
      <SideBarButton onClick={logout}>
        <TbLogout size={22} />
        <TitleOption show={showOptions ? 1 : 0}>Sair</TitleOption>
      </SideBarButton>
    </StyledSideBar>
  );
}
