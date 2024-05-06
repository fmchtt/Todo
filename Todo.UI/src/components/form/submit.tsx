import FilledButton from "../filledButton";

type SubmitProps = {
  label: string;
  $loading?: boolean;
};
export default function SubmitButton({ label, $loading }: SubmitProps) {
  return (
    <FilledButton type="submit" $loading={$loading}>
      {label}
    </FilledButton>
  );
}
