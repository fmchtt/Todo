import styled from "styled-components";

export const CardContainer = styled.div`
  background: ${(props) => props.theme.colors[600]};

  padding: 10px 30px;
  border-radius: 20px;

  position: relative;

  cursor: pointer;

  :hover {
    background: ${(props) => props.theme.colors[600] + "c1"};
  }
`;

export const CardGroup = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;

  color: ${(props) => props.theme.font.medium};
`;

export const CardFooter = styled.div`
  margin-top: 8px;

  display: flex;
  justify-content: flex-end;
`;

export const LeftDecoration = styled.span`
  background: ${(props) => props.theme.colors[100]};
  border-radius: 20px 0 0 20px;

  position: absolute;
  left: 0;
  top: 0;

  display: block;

  height: 100%;
  width: 16px;
`;
