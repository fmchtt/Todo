import styled from "styled-components";

export const Container = styled.div`
  height: 100%;
`;

export const HeadingContainer = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
`;

export const ActionsContainer = styled.div`
  color: ${(props) => props.theme.font.medium};
  display: flex;
  gap: 15px;
`;

export const ColumnContainer = styled.div`
  height: 94%;
  margin: 15px 0;

  display: flex;
  gap: 30px;
`;
