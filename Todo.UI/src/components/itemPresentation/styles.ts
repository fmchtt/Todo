import styled from "styled-components";
import { ComponentPropsWithoutRef } from "react";

export const PresentationContainer = styled.div`
  background: ${(props) => props.theme.gradients.full};
  border-radius: 20px;
  display: flex;

  width: clamp(800px, 70vw, 90vw);
  height: 80vh;

  box-shadow: 0 0 16px 4px rgba(255, 255, 255, 0.08);
`;

export const PresentationBody = styled.div`
  padding: 20px 20px 0 20px;
  flex: 1;
  overflow-y: auto;
`;

type DetailsContainerProps = ComponentPropsWithoutRef<"div">;
export const DetailsContainer = styled.div<DetailsContainerProps>`
  padding: 5px;
  color: ${(props) => props.theme.font.medium};
  min-height: calc(100% - 195px);
  border-radius: 20px;
  cursor: pointer;
  margin-bottom: 20px;

  &:hover {
    border: 1px solid rgba(255, 255, 255, 0.2);
  }

  * {
    color: ${(props) => props.theme.font.medium};
  }
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
  $flex?: boolean;
};
export const PresentationGroup = styled.div<PresentationGroupProps>`
  color: ${(props) => props.theme.font.medium};
  display: ${(props) => (props.$flex ? "flex" : "block")};
  justify-content: space-between;
  align-items: center;
  gap: 40px;
`;

type PresentationDataGroupProps = {
  $padding?: string;
};
export const PresentationDataGroup = styled.div<PresentationDataGroupProps>`
  background-color: ${(props) => props.theme.colors[400]};
  cursor: pointer;

  :hover {
    background: ${(props) => props.theme.colors[400] + "c1"};
  }

  display: flex;
  align-items: center;
  gap: 10px;

  border-radius: 20px;
  padding: ${(props) => props.$padding || "2px"};
`;

export const PresentationDataGroupSelect = styled.select`
  background-color: ${(props) => props.theme.colors[400]};
  color: inherit;
  font-size: inherit;
  border: none;
  width: 100%;
`;
