import { ComponentPropsWithoutRef } from "react";

export interface StyledButtonProps extends ComponentPropsWithoutRef<"button"> {}

export interface FilledButtonProps {
  children: string;
  loading?: boolean;
  size?: string;
  disabled?: boolean;
  type?: "button" | "reset" | "submit" | undefined;
}
