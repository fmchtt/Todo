import styled from "styled-components";

type Arrow = {
  direction: string;
};

export const Container = styled.div`
  display: flex;
  flex-direction: column;
`;

export const Section = styled.div`
  display: flex;
  flex-direction: column;
`;

export const TitleSection = styled.h2`
  color: #fff;
  font-weight: 400;
`;

export const Carousel = styled.div`
  display: flex;
  overflow-x: auto;
  gap: 20px;
  transition: 0.3s;
  margin-top: 20px;
`;

export const Arrow = styled.button<Arrow>`
  background-color: green;
  color: #fff;
  padding: 20px;
  position: absolute;
  right: ${(props) => {
    if (props.direction === "right") return "0";
    else return;
  }};
  left: ${(props) => {
    if (props.direction === "left") return "0";
    else return;
  }};
  top: 50%;
  transform: translateY(-50%);
`;

export const HeadingContainer = styled.div`
  display: flex;
  justify-content: space-between;
`;

export const ActionButton = styled.div`
  cursor: pointer;

  display: flex;
  align-items: center;
  justify-content: center;
  gap: 5px;
`;

export const ActionContainer = styled.div`
  color: ${(props) => props.theme.font.medium};

  display: flex;
  align-items: center;
  justify-content: center;
  gap: 5px;
`;
