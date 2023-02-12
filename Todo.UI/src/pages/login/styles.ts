import styled from "styled-components";

export const Container = styled.div`
  background-image: ${(props) => props.theme.gradients.full};
  width: 100vw;
  height: 100vh;
  display: flex;
`;

export const Side = styled.div`
  position: relative;
  width: 100%;
  flex: 2;
  height: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-evenly;

  img {
    max-width: 400px;
    width: 100%;
  }

  @media (max-width: 900px) {
    display: none;
  }
`;

export const ContentTextSide = styled.div`
  display: flex;
  flex-direction: column;

  h1,
  h3 {
    font-weight: 400;
  }

  p {
    font-weight: 200;
  }
`;
