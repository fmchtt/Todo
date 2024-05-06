import styled from "styled-components";

export const H1 = styled.h1`
  color: ${(props) => props.theme.font.bold};
  font-size: 2em;
`;

export const H2 = styled.h2`
  color: ${(props) => props.theme.font.bold};
`;

interface TextProps {
  $margin?: string;
  $dashed?: boolean;
  $weight?: 200 | 300 | 400 | 500 | 600 | 700 | 800 | 900;
  $size?: "thin" | "medium" | "large";
  $lineLimiter?: boolean;
  $color?: string;
}

export const Text = styled.p<TextProps>`
  color: ${(props) => props.color || props.theme.font.bold};
  font-weight: ${(props) => props.$weight || 300};
  font-size: ${(props) => {
    switch (props.$size) {
      case "thin":
        return "0.8em";
      case "medium":
        return "1em";
      case "large":
        return "1.6em";
      default:
        return "1em";
    }
  }};
  margin: ${(props) => props.$margin || "inherit"};
  text-decoration: ${(props) => (props.$dashed ? "line-through" : "none")};

  flex: ${(props) => (props.$lineLimiter ? "1" : "unset")};
  white-space: ${(props) => (props.$lineLimiter ? "nowrap" : "unset")};
  overflow: ${(props) => (props.$lineLimiter ? "hidden" : "unset")};
  text-overflow: ${(props) => (props.$lineLimiter ? "ellipsis" : "unset")};
`;

export const LinkSpan = styled.span`
  color: ${(props) => props.theme.colors[100]};
  font-weight: 300;
  text-decoration: none;
`;
