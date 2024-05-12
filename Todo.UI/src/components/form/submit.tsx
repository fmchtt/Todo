import FilledButton from "../filledButton";
import { StyledButtonProps } from "../filledButton/types";

type SubmitProps = StyledButtonProps & {
  label: string;
  $loading?: boolean;
};
export default function SubmitButton({
  label,
  $loading,
  ...props
}: SubmitProps) {
  return (
    <FilledButton {...props} type="submit" $loading={$loading}>
      {label}
    </FilledButton>
  );
}
