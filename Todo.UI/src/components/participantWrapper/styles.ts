import styled from "styled-components";

export const StyledParticipantWrapper = styled.div`
  margin: 0;
  padding: 5px;
  border-radius: 50%;

  display: flex;
  justify-content: center;
  align-items: center;

  cursor: pointer;

  :hover {
    background: rgba(255, 255, 255, 0.09);
  }
`;

export const RoudedInitials = styled.div`
  width: 40px;
  height: 40px;
  border-radius: 50%;
`;
