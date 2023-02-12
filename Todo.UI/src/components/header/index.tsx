import { H2, Text } from "../../assets/css/global.styles";
import useAuth from "../../context/auth";
import { StyledHeader, UserMenu } from "./styles";

export default function Header() {
  const { user } = useAuth();

  return (
    <StyledHeader>
      <H2>Taskerizer</H2>
      <UserMenu>
        <Text>{user?.name}</Text>
      </UserMenu>
    </StyledHeader>
  );
}
