import { StyledRoundedAvatar, RoundedInitials } from "./styles";
import profilePlaceholder from "@/assets/images/profile.svg";

interface RoundedAvatarProps {
  name?: string;
  avatarUrl?: string;
  $over?: boolean;
  $size?: number;
}

export default function RoundedAvatar(props: RoundedAvatarProps) {
  if (props.avatarUrl) {
    return (
      <StyledRoundedAvatar
        $size={props.$size}
        $over={props.$over}
        src={props.avatarUrl}
      />
    );
  }

  if (props.name) {
    return (
      <RoundedInitials $over={props.$over} $size={props.$size}>
        {props.name
          .split(" ")
          .reduce((prev, current) => `${prev[0]}. ${current[0]}.`)
          .toUpperCase()}
      </RoundedInitials>
    );
  }

  return <StyledRoundedAvatar $over={props.$over} src={profilePlaceholder} />;
}
