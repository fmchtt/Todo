import User from "@/types/user";
import { RoundedAvatar } from "../header/styles";
import { RoudedInitials, StyledParticipantWrapper } from "./styles";

interface ParticipantWrapperProps {
  participants: User[];
  onClick?: () => void;
}
export default function ParticipantWrapper(props: ParticipantWrapperProps) {
  return (
    <StyledParticipantWrapper onClick={props.onClick}>
      {props.participants.slice(0, 5).map((participant, idx) => {
        if (participant.avatarUrl) {
          return (
            <RoundedAvatar
              over={idx > 0 ? 1 : 0}
              key={participant.id}
              src={import.meta.env.VITE_API_URL + participant.avatarUrl}
            />
          );
        }

        return (
          <RoudedInitials key={participant.id} over={idx > 0 ? 1 : 0}>
            {participant.name
              .split(" ")
              .reduce((prev, current) => `${prev[0]}. ${current[0]}.`)
              .toUpperCase()}
          </RoudedInitials>
        );
      })}
      {props.participants.length > 5 && (
        <RoudedInitials>+{props.participants.length - 5}</RoudedInitials>
      )}
    </StyledParticipantWrapper>
  );
}
