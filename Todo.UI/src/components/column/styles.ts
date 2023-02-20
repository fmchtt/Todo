import styled from "styled-components";

export const ColumnStyled = styled.div`
  background: ${(props) => props.theme.colors[800]};
  width: 300px;
  height: 100%;

  border-radius: 20px;
`;

export const ColumnHeading = styled.div`
  background: ${(props) => props.theme.colors[700]};
  color: ${(props) => props.theme.font.medium};

  border-radius: 20px 20px 0 0;
  padding: 18px;

  display: flex;
  justify-content: space-between;
  align-items: center;
`;

export const ColumnBody = styled.div`
  padding: 10px;
`;