import ConfirmationModal from "../../components/forms/ConfirmationForm";
import useModal from "../useModal";

type useConfirmationModalProps = {
  message: string;
  onConfirm: () => void;
};
export default function useConfirmationModal(props: useConfirmationModalProps) {
  return useModal(
    <ConfirmationModal label={props.message} onConfirm={props.onConfirm} />
  );
}
