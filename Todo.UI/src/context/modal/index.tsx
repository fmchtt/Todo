import { createContext, ReactNode, useContext, useReducer } from "react";
import { ContainerModal, ModalStyled } from "./styles";
import { TbX } from "react-icons/tb";

type ModalContextProps = {
  isOpen: boolean;
  modalId: string;
  closeModal: () => void;
  openModal: (withControls: boolean, component: ReactNode, id: string) => void;
};

const modalContext = createContext<ModalContextProps>({} as ModalContextProps);

type ModalProviderProps = {
  children: ReactNode;
};

type ModalState = {
  withControls: boolean;
  component: ReactNode;
  modalId: string;
  isOpen: boolean;
};

type ModalAction = {
  component?: ReactNode;
  withControls?: boolean;
  modalId?: string;
  type: "OPEN" | "CLOSE";
};

export function ModalProvider({ children }: ModalProviderProps) {
  function reducer(state: ModalState, action: ModalAction): ModalState {
    console.log(state, action);

    if (action.type == "OPEN") {
      return {
        component: action.component,
        withControls: action.withControls == null ? true : action.withControls,
        modalId: action.modalId || "",
        isOpen: !!action.component,
      };
    }

    return { isOpen: false, component: null, withControls: true, modalId: "" };
  }

  const [state, dispatch] = useReducer(reducer, {
    isOpen: false,
    modalId: "",
    withControls: true,
    component: null,
  });

  function openModal(withControls: boolean, component: ReactNode, id: string) {
    console.log(id, state);

    dispatch({
      component: component,
      withControls: withControls,
      modalId: id,
      type: "OPEN",
    });
  }

  function closeModal() {
    console.log(state);

    dispatch({ type: "CLOSE" });
  }

  return (
    <modalContext.Provider
      value={{
        isOpen: state.isOpen,
        modalId: state.modalId,
        closeModal: closeModal,
        openModal: openModal,
      }}
    >
      {state.isOpen && (
        <ContainerModal>
          {state.withControls ? (
            <ModalStyled>
              <TbX
                role="button"
                color="#fff"
                cursor="pointer"
                size={30}
                onClick={closeModal}
              />
              {state.component}
            </ModalStyled>
          ) : (
            state.component
          )}
        </ContainerModal>
      )}
      {children}
    </modalContext.Provider>
  );
}

export function useModalContext() {
  return useContext(modalContext);
}
