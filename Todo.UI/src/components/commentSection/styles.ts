import styled from "styled-components";

export const CommentSectionContainer = styled.div`
  padding-top: 10px;
  border-top: 1px solid ${(props) => props.theme.colors[200]};
`;

export const CommentLineContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: 10px;
  margin: 24px 0;
`;

export const CommentLine = styled.div`
  padding: 10px;
  border-bottom: 1px solid ${(props) => props.theme.colors[200]};
`;

export const UserContainer = styled.div`
  display: flex;
  align-items: center;
  margin-bottom: 5px;
  gap: 5px;
`;

export const CommentHead = styled.div`
  display: flex;
  justify-content: space-between;
`;

export const CommentControls = styled.div`
  color: ${(props) => props.theme.font.medium};
  display: flex;
  gap: 10px;
`;
