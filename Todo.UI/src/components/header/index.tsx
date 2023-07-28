import { H2, Text } from "@/assets/css/global.styles";
import useAuth from "@/context/auth";
import {
  DropDownButton,
  DropDownMenu,
  StyledHeader,
  UserContainer,
  UserMenu,
} from "./styles";
import { Link } from "react-router-dom";
import RoundedAvatar from "@/components/roundedAvatar";
import useDebounce from "@/hooks/useDebounce";

export default function Header() {
  const { user } = useAuth();
  const [dropDownOpen, setDropdownOpen, isClosing] = useDebounce(false, 200);

  return (
    <StyledHeader>
      <H2>Taskerizer</H2>
      <UserMenu
        onClick={() => setDropdownOpen(true, true)}
        onMouseLeave={() => setDropdownOpen(false)}
      >
        {user && (
          <UserContainer>
            <Text margin="0 10px">{user?.name}</Text>
            <RoundedAvatar
              avatarUrl={
                user?.avatarUrl && import.meta.env.VITE_API_URL + user.avatarUrl
              }
            />
          </UserContainer>
        )}
        {dropDownOpen && (
          <DropDownMenu closing={isClosing}>
            <DropDownButton as={Link} to="/profile">
              Editar Perfil
            </DropDownButton>
          </DropDownMenu>
        )}
      </UserMenu>
    </StyledHeader>
  );
}
