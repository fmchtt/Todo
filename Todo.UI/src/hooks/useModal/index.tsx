import { ReactNode } from "react";
import { createPortal } from "react-dom";
import { TbX } from "react-icons/tb";
import useDebounce from "../useDebounce";
import { ContainerModal, ModalStyled } from "./styles";

type ModalProps = {
  isOpen: boolean;
  withControls: boolean;
  closeModal: () => void;
  component: ReactNode;
  isClosing: boolean;
};

function Modal({
  isOpen,
  withControls,
  closeModal,
  component,
  isClosing,
}: ModalProps) {
  if (isOpen) {
    return createPortal(
      <ContainerModal $closing={isClosing}>
        {withControls ? (
          <ModalStyled>
            <TbX
              role="button"
              color="#fff"
              cursor="pointer"
              size={30}
              onClick={closeModal}
            />
            {component}
          </ModalStyled>
        ) : (
          component
        )}
      </ContainerModal>,
      document.body,
    );
  }

  return null;
}

export default function useModal(
  component: ReactNode,
  withControls = true,
): [ReactNode, () => void, () => void] {
  const [isOpen, setOpen, isDebouncing] = useDebounce(false, 200);

  function closeModal() {
    setOpen(false);
  }

  function openModal() {
    setOpen(true, true);
  }

  return [
    <Modal
      key={crypto.randomUUID()}
      isOpen={isOpen}
      withControls={withControls}
      closeModal={closeModal}
      component={component}
      isClosing={isDebouncing}
    />,
    openModal,
    closeModal,
  ];
}
