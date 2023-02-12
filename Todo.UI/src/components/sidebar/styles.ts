import styled from "styled-components";

export const StyledSideBar = styled.aside`
  background: ${(props) => props.theme.gradients.semi};

  width: 220px;
  padding: 30px 0;

  display: flex;
  flex-direction: column;
  align-items: center;
`;

export const ButtonGroup = styled.div`
  width: 100%;
  flex: 1;
`;

export const SideBarButton = styled.a`
  text-decoration: none;
  color: ${(props) => props.theme.font.medium};
  font-weight: 300;
  cursor: pointer;

  width: 100%;
  padding: 15px 30px;

  display: flex;
  align-items: center;
  gap: 10px;

  :hover {
    background: #ffffff11;
  }
`;
