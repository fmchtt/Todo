import { ReactNode, useState } from "react";
import { ContainerModal, ModalStyled } from "./styles";
import { createPortal } from "react-dom";
import { TbX } from "react-icons/tb";

export default function useModal(
  component: ReactNode,
  initialState = false,
  withControls = true
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
            {withControls ? (
              <ModalStyled>
                <TbX
                  role="button"
                  color="#fff"
                  cursor="pointer"
                  size={30}
                  onClick={handleState}
                />
                {component}
              </ModalStyled>
            ) : (
              component
            )}
          </ContainerModal>,
          document.body
        )}
    </>,
  ];
}
