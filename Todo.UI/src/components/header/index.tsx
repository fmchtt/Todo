import { useState } from "react";
import { H2, Text } from "../../assets/css/global.styles";
import useAuth from "../../context/auth";
import {
  DropDownButton,
  DropDownMenu,
  RoundedAvatar,
  StyledHeader,
  UserContainer,
  UserMenu,
} from "./styles";
import { Link } from "react-router-dom";

export default function Header() {
  const { user } = useAuth();
  const [dropDownOpen, setDropdownOpen] = useState(false);

  return (
    <StyledHeader>
      <H2>Taskerizer</H2>
      <UserMenu
        onClick={() => setDropdownOpen(true)}
        onMouseLeave={() => setDropdownOpen(false)}
      >
        <UserContainer>
          <Text margin="0 10px">{user?.name}</Text>
          {user?.avatarUrl && (
            <RoundedAvatar
              src={import.meta.env.VITE_API_URL + user.avatarUrl}
            />
          )}
        </UserContainer>
        {dropDownOpen && (
          <DropDownMenu>
            <DropDownButton as={Link} to="/profile">
              Editar Perfil
            </DropDownButton>
          </DropDownMenu>
        )}
      </UserMenu>
    </StyledHeader>
  );
}
