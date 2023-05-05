import { ReactNode, useState } from "react";
import { createPortal } from "react-dom";
import { TbX } from "react-icons/tb";
import { ContainerModal, ModalStyled } from "./styles";

type ModalProps = {
  isOpen: boolean;
  withControls: boolean;
  closeModal: () => void;
  component: ReactNode;
};

function Modal({ isOpen, withControls, closeModal, component }: ModalProps) {
  if (isOpen) {
    return createPortal(
      <ContainerModal>
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
        )
      </ContainerModal>,
      document.body
    );
  }

  return null;
}

export default function useModal(
  component: ReactNode,
  withControls = true
): [ReactNode, () => void, () => void] {
  const [isOpen, setOpen] = useState(false);

  function closeModal() {
    setOpen(false);
  }

  function openModal() {
    setOpen(true);
  }

  return [
    <Modal
      key={crypto.randomUUID()}
      isOpen={isOpen}
      withControls={withControls}
      closeModal={closeModal}
      component={component}
    />,
    openModal,
    closeModal,
  ];
}
