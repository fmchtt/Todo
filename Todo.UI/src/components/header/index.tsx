import { H2, Text } from "../../assets/css/global.styles";
import useAuth from "../../context/auth";
import { RoundedAvatar, StyledHeader, UserMenu } from "./styles";

export default function Header() {
  const { user } = useAuth();

  return (
    <StyledHeader>
      <H2>Taskerizer</H2>
      <UserMenu>
        <Text margin="0 10px">{user?.name}</Text>
        {user?.avatarUrl && (
          <RoundedAvatar src={import.meta.env.VITE_API_URL + user.avatarUrl} />
        )}
      </UserMenu>
    </StyledHeader>
  );
}
