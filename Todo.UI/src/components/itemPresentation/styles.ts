import styled from "styled-components";

export const PresentationContainer = styled.div`
  background: ${(props) => props.theme.gradients.full};
  border-radius: 20px;
  display: flex;
  gap: 20px;

  width: clamp(800px, 70vw, 90vw);
  height: 80vh;
`;

export const PresentationBody = styled.div`
  padding: 20px 20px 0 20px;
  flex: 1;
  overflow-y: auto;
`;

export const PresentationSide = styled.div`
  background: ${(props) => props.theme.gradients.semi};
  width: 270px;
  padding: 20px;

  display: flex;
  flex-direction: column;
  gap: 10px;

  border-radius: 0 20px 20px 0;
`;

type PresentationGroupProps = {
  flex?: boolean;
};
export const PresentationGroup = styled.div<PresentationGroupProps>`
  color: ${(props) => props.theme.font.medium};
  display: ${(props) => (props.flex ? "flex" : "block")};
  justify-content: space-between;
  align-items: center;
`;

type PresentationDataGroupProps = {
  padding?: string;
};
export const PresentationDataGroup = styled.div<PresentationDataGroupProps>`
  background: ${(props) => props.theme.colors[400]};
  cursor: pointer;

  :hover {
    background: ${(props) => props.theme.colors[400] + "c1"};
  }

  display: flex;
  align-items: center;
  gap: 10px;

  border-radius: 20px;
  padding: ${(props) => props.padding || "2px"};
`;
