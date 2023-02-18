import styled from "styled-components";

export const StyledHeader = styled.header`
  background: ${(props) => props.theme.colors[500]};

  height: 60px;
  padding: 0 30px;

  display: flex;
  align-items: center;
  justify-content: space-between;
`;

export const UserMenu = styled.div`
  background: ${(props) => props.theme.colors[200]};
  cursor: pointer;

  :hover {
    background: ${(props) => props.theme.colors[200] + "c1"};
  }

  border-radius: 20px;
  padding: 8px 20px;
`;
