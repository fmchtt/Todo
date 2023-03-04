import styled from "styled-components";

type RoundedAvatarProps = {
  over?: number;
};
export const StyledRoundedAvatar = styled.img<RoundedAvatarProps>`
  width: 40px;
  height: 40px;
  border-radius: 50%;

  margin-left: ${(props) => (props.over ? "-16px" : "unset")};
`;

type RoundedInitialsProps = {
  over?: number;
};
export const RoundedInitials = styled.div<RoundedInitialsProps>`
  width: 40px;
  height: 40px;

  display: flex;
  justify-content: center;
  align-items: center;

  background: ${(props) => props.theme.colors[400]};
  color: ${(props) => props.theme.font.medium};
  font-size: 0.8em;
  border-radius: 50%;

  margin-left: ${(props) => (props.over ? "-16px" : "unset")};
`;
