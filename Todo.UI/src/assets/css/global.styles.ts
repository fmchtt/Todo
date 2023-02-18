import styled from "styled-components";

export const H1 = styled.h1`
  color: ${(props) => props.theme.font.bold};
  font-size: 2em;
`;

export const H2 = styled.h2`
  color: ${(props) => props.theme.font.bold};
`;

export const Text = styled.p`
  color: ${(props) => props.theme.font.bold};
  font-weight: 300;
`;

export const A = styled.a`
  color: ${(props) => props.theme.colors[100]};
  font-weight: 300;
  text-decoration: none;
`;
