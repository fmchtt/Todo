import User from "@/types/user";
import { StyledParticipantWrapper } from "./styles";
import RoundedAvatar from "@/components/roundedAvatar";
import { RoundedInitials } from "@/components/roundedAvatar/styles";

interface ParticipantWrapperProps {
  participants: User[];
  onClick?: () => void;
}

export default function ParticipantWrapper(props: ParticipantWrapperProps) {
  return (
    <StyledParticipantWrapper onClick={props.onClick}>
      {props.participants.slice(0, 5).map((participant, idx) => {
        return (
          <RoundedAvatar
            $over={idx > 0}
            key={participant.id}
            avatarUrl={
              participant.avatarUrl &&
              import.meta.env.VITE_API_URL + participant.avatarUrl
            }
            name={participant.name}
          />
        );
      })}

      {props.participants.length > 5 && (
        <RoundedInitials>+{props.participants.length - 5}</RoundedInitials>
      )}
    </StyledParticipantWrapper>
  );
}
