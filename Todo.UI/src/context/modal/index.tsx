import { createContext, ReactNode, useState, useEffect } from "react";
import { ContainerModal, ModalStyled } from "./styles";
import { TbX } from "react-icons/tb";

type ModalContextProps = {
  isOpen: boolean;
  closeModal: () => void;
  openModal: (withControls: boolean, component: ReactNode) => void;
};

export const modalContext = createContext<ModalContextProps>(
  {} as ModalContextProps
);

type ModalProviderProps = {
  children: ReactNode;
};

export function ModalProvider({ children }: ModalProviderProps) {
  const [component, setComponent] = useState<ReactNode>();
  const [withControls, setWithControls] = useState<boolean>(true);
  const [isOpen, setIsOpen] = useState<boolean>(false);

  function openModal(controls: boolean, component: ReactNode) {
    if (withControls !== controls) {
      setWithControls(controls);
    }
    if (component) {
      setComponent(component);
    }
  }

  function closeModal() {
    setComponent(null);
  }

  useEffect(() => {
    if (component) {
      setIsOpen(true);
    } else {
      setIsOpen(false);
    }
  }, [component]);

  return (
    <modalContext.Provider
      value={{
        isOpen: isOpen,
        closeModal: closeModal,
        openModal: openModal,
      }}
    >
      {isOpen && (
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
        </ContainerModal>
      )}
      {children}
    </modalContext.Provider>
  );
}
