import { ReactNode, useContext } from "react";
import { modalContext } from "@/context/modal";

export default function useModal(component: ReactNode, withControls = true) {
  const { isOpen, closeModal, openModal } = useContext(modalContext);

  if (isOpen == undefined) {
    throw new Error("Contexto de Modal não inicializado!");
  }

  function modalOpen() {
    openModal(withControls, component);
  }

  return [modalOpen, closeModal];
}
