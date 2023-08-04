import styled from "styled-components";

type RoundedAvatarProps = {
  over?: number;
  size?: number;
};
export const StyledRoundedAvatar = styled.img<RoundedAvatarProps>`
  width: ${(props) => (props.size ? `${props.size}px` : "40px")};
  height: ${(props) => (props.size ? `${props.size}px` : "40px")};
  border-radius: 50%;

  margin-left: ${(props) => (props.over ? "-16px" : "unset")};
`;

type RoundedInitialsProps = {
  over?: number;
  size?: number;
};
export const RoundedInitials = styled.div<RoundedInitialsProps>`
  width: ${(props) => (props.size ? `${props.size}px` : "40px")};
  height: ${(props) => (props.size ? `${props.size}px` : "40px")};

  display: flex;
  justify-content: center;
  align-items: center;

  background: ${(props) => props.theme.colors[400]};
  color: ${(props) => props.theme.font.medium};
  font-size: 0.8em;
  border-radius: 50%;

  margin-left: ${(props) => (props.over ? "-16px" : "unset")};
`;
