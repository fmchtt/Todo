import { DefaultTheme, StyledComponentProps } from "styled-components";

export interface FormBoardProps
  extends StyledComponentProps<"form", DefaultTheme, {}, never> {
  maxWidth?: string;
  borderRadius?: string;
  closeModal?: () => void;
}
