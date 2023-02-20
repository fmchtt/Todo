import styled from "styled-components";

export const CardContainer = styled.div`
  background-color: ${(props) => props.theme.colors[200]};
  width: 260px;
  flex: none;
  transition: 0.3s;
  border-left: 14px solid ${(props) => props.theme.colors[100]};
  border-radius: 10px;
  padding: 10px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;

  cursor: pointer;
  :hover {
    background-color: ${(props) => props.theme.colors[200] + "c1"};
  }
`;

export const CardTitle = styled.h3`
  color: #fff;
  font-weight: 400;
`;

export const CardSubtitle = styled.p`
  color: #fff;
  font-weight: 200;
  font-size: 14px;
`;

export const CardHeading = styled.div`
  display: flex;
  flex-direction: column;
  gap: 4px;
  flex: 1;
`;

export const CardBody = styled.div`
  display: flex;
  flex-direction: column;
  flex: 1;
  text-align: center;
  justify-content: space-between;
`;

export const DataContainer = styled.div`
  display: flex;
`;

export const DataGroup = styled.div`
  width: 50%;
  height: 80px;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
`;
