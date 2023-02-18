import styled from "styled-components";

type Arrow = {
  direction: string;
};

export const Container = styled.div`
  display: flex;
  flex-direction: column;
  gap: 20px;
`;

export const Section = styled.div`
  width: 100%;
  padding: 20px;
  display: flex;
  flex-direction: column;
  position: relative;
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

export const Board = styled.div`
  background-color: ${(props) => props.theme.colors[200]};
  width: 240px;
  height: 240px;
  flex: none;
  transition: 0.3s;
  border-left: 14px solid ${(props) => props.theme.colors[100]};
  border-radius: 10px;
  padding: 10px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
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

export const TitleBoard = styled.h3`
  color: #fff;
  font-weight: 400;
`;

export const SubtitleBoard = styled.p`
  color: #fff;
  font-weight: 200;
  font-size: 14px;
`;

export const DivTitle = styled.div`
  display: flex;
  flex-direction: column;
  gap: 4px;
  flex: 1;
`;

export const DivStatus = styled.div`
  display: flex;
  flex-direction: column;
  flex: 1;
  text-align: center;
  justify-content: space-between;
`;

export const Status = styled.div`
  display: flex;
`;

export const StatusSingle = styled.div`
  width: 50%;
  height: 80px;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
`;

export const Counter = styled.p`
  color: #fff;
  font-weight: 400;
  font-size: 14px;
`;

export const ButtonAddBoard = styled.div`
  display: flex;
  gap: 5px;
  align-items: center;
  justify-content: center;
  width: fit-content;
  cursor: pointer;
  position: absolute;
  right: 20px;
  margin-top: 20px;
  z-index: 1;

  .icon-plus {
    color: #fff;
    font-size: 35px;
  }
`;
