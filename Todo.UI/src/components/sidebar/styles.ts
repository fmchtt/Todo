import styled from "styled-components";

interface Options {
  show?: boolean;
}

export const StyledSideBar = styled.aside`
  background: ${(props) => props.theme.gradients.semi};

  height: calc(100vh - 60px);
  padding: 30px 0;

  display: flex;
  flex-direction: column;
  align-items: center;
`;

export const ButtonGroup = styled.div`
  width: 100%;
  flex: 1;

  .icon-menu {
    color: ${(props) => props.theme.font.medium};
    width: 25px;
    height: 30px;
    margin-left: 10px;
    cursor: pointer;
    display: none;

    @media (max-width: 600px) {
      display: flex;
    }
  }
`;

export const SideBarButton = styled.a<Options>`
  text-decoration: none;
  color: ${(props) => props.theme.font.medium};
  width: 100%;
  padding: 10px 30px;
  cursor: pointer;

  display: flex;
  align-items: center;
  gap: 10px;

  :hover {
    background: #ffffff11;
  }

  @media (max-width: 600px) {
    padding: ${(props) => (props.show ? "15px 25px 15px 10px" : "15px 10px")};
  }
`;

export const TitleOption = styled.p<Options>`
  color: ${(props) => props.theme.font.medium};
  font-weight: 300;
  cursor: pointer;

  @media (max-width: 600px) {
    display: ${(props) => (props.show ? "flex" : "none")};
  }
`;
