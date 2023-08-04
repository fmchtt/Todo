import { ComponentPropsWithoutRef } from "react";

export type StyledButtonProps = ComponentPropsWithoutRef<"button"> &
  FilledButtonProps;

export interface FilledButtonProps {
  loading?: number;
  size?: string;
  disabled?: boolean;
  type?: "button" | "reset" | "submit" | undefined;
  margin?: string;
}
