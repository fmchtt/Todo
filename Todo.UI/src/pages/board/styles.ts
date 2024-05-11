import styled from "styled-components";

export const Container = styled.div`
  display: flex;
  flex-direction: column;
  height: 100%;
`;

export const HeadingContainer = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
`;

type ActionsContainerProps = {
  $clickable?: boolean;
};
export const ActionsContainer = styled.div<ActionsContainerProps>`
  color: ${(props) => props.theme.font.medium};
  cursor: ${(props) => (props.$clickable ? "pointer" : "unset")};
  gap: ${(props) => (props.$clickable ? "2px" : "15px")};

  display: flex;
  align-items: center;
`;

export const ColumnContainer = styled.div`
  margin: 15px 0;
  flex: 1;

  display: flex;
  gap: 30px;
`;

type ColumnDroppableAreaProps = {
  $hover: boolean;
  $show: boolean;
};
export const ColumnDroppableArea = styled.div<ColumnDroppableAreaProps>`
  display: ${(props) => (props.$show ? "block" : "none")};
  width: 30px;
  background-color: rgba(
    255,
    255,
    255,
    ${(props) => (props.$hover ? 0.3 : 0.1)}
  );
  border-radius: 20px;
`;
