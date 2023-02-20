import { ComponentPropsWithoutRef } from "react";

export type StyledButtonProps = ComponentPropsWithoutRef<"button">;

export interface FilledButtonProps {
  children: string;
  loading?: number;
  size?: string;
  disabled?: boolean;
  type?: "button" | "reset" | "submit" | undefined;
}
