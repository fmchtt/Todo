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
  cursor: pointer;
  position: relative;
`;

export const UserContainer = styled.div`
  background: ${(props) => props.theme.colors[200]};
  border-radius: 20px;
  padding: 2px;

  :hover {
    background: ${(props) => props.theme.colors[100]};
  }
  position: relative;

  display: flex;
  align-items: center;
  z-index: 2;
`;

type MenuProps = {
  closing: boolean;
};
export const DropDownMenu = styled.div<MenuProps>`
  background: ${(props) => props.theme.colors[300]};
  box-shadow: 0px 0px 6px 2px rgba(0, 0, 0, 0.25);

  padding: 30px 0 10px 0;

  position: absolute;
  top: 30px;
  left: 0;

  width: 100%;
  border-radius: 0 0 20px 20px;
  z-index: 1;

  ${(props) => {
    return props.closing
      ? "animation: fade-up 200ms linear;"
      : "animation: fade-down 200ms linear;";
  }}
`;

export const DropDownButton = styled.a`
  color: ${(props) => props.theme.font.medium};
  text-decoration: none;

  display: flex;
  justify-content: center;
  align-items: center;

  width: 100%;
  padding: 10px 0;

  :hover {
    background: ${(props) => props.theme.colors[800]};
  }
`;
