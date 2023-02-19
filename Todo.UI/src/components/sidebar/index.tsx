import { Link } from "react-router-dom";
import {
  ButtonGroup,
  SideBarButton,
  StyledSideBar,
  TitleOption,
} from "./styles";
import { FaTasks } from "react-icons/fa";
import { BsCalendar2Range } from "react-icons/bs";
import { IoExitOutline, IoHomeOutline, IoMenuSharp } from "react-icons/io5";
import useAuth from "../../context/auth";
import { ReactNode, useState } from "react";

interface Options {
  icon: ReactNode;
  text: string;
  route?: string;
  action?: () => void;
}

export default function SideBar() {
  const [showOptions, setShowOptions] = useState<boolean>(false);
  const { logout } = useAuth();

  const options: Options[] = [
    {
      icon: <IoHomeOutline size={26} />,
      text: "Dashboard",
      route: "/home",
    },
    {
      icon: <BsCalendar2Range size={26} />,
      text: "Quadros",
      route: "/home",
    },
    {
      icon: <FaTasks size={26} />,
      text: "Tarefas",
      route: "/home",
    },
  ];

  return (
    <StyledSideBar>
      <ButtonGroup>
        <IoMenuSharp
          className="icon-menu"
          onClick={() => {
            setShowOptions(showOptions ? false : true);
          }}
        />
        {options.map((option, index) => {
          return option.action ? (
            <SideBarButton
              show={showOptions}
              key={index}
              onClick={option.action}
            >
              {option.icon}
              <TitleOption show={showOptions}>{option.text}</TitleOption>
            </SideBarButton>
          ) : (
            <SideBarButton show={showOptions} key={index} as={Link} to="/home">
              {option.icon}
              <TitleOption show={showOptions}>{option.text}</TitleOption>
            </SideBarButton>
          );
        })}
      </ButtonGroup>
      <SideBarButton onClick={logout}>
        <IoExitOutline size={26} />
        <TitleOption show={showOptions}>Sair</TitleOption>
      </SideBarButton>
    </StyledSideBar>
  );
}
