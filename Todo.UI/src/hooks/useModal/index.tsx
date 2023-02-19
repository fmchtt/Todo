import { ReactNode, useState } from "react";
import { ContainerModal, ModalStyled } from "./styles";
import { createPortal } from "react-dom";
import { IoClose } from "react-icons/io5";

export default function useModal(
  component: ReactNode,
  initialState = false
): [() => void, ReactNode] {
  const [open, setOpen] = useState(initialState);

  function handleState() {
    setOpen((state) => !state);
  }

  return [
    handleState,
    <>
      {open &&
        createPortal(
          <ContainerModal>
            <ModalStyled>
              <IoClose
                role="button"
                color="#fff"
                cursor="pointer"
                size={30}
                onClick={handleState}
              />
              {component}
            </ModalStyled>
          </ContainerModal>,
          document.body
        )}
    </>,
  ];
}
