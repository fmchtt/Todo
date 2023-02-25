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
      {props.participants.map((participant) => {
        if (participant.avatarUrl) {
          return (
            <RoundedAvatar
              key={participant.id}
              src={import.meta.env.VITE_API_URL + participant.avatarUrl}
            />
          );
        }

        return (
          <RoudedInitials key={participant.id}>
            {participant.name
              .split(" ")
              .reduce((prev, current) => `${prev} ${current}.`)
              .toUpperCase()}
          </RoudedInitials>
        );
      })}
    </StyledParticipantWrapper>
  );
}
