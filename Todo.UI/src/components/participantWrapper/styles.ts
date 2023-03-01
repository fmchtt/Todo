import styled from "styled-components";

export const StyledParticipantWrapper = styled.div`
  margin: 0;
  padding: 5px;
  border-radius: 20px;

  display: flex;
  justify-content: center;
  align-items: center;

  cursor: pointer;

  :hover {
    background: rgba(255, 255, 255, 0.09);
  }
`;

type RoudedInitialsProps = {
  over?: number;
};
export const RoudedInitials = styled.div<RoudedInitialsProps>`
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
