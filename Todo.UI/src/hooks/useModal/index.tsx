import { ReactNode, useState } from "react";
import { useModalContext } from "@/context/modal";

export default function useModal(
  component: ReactNode,
  withControls = true
): () => void {
  const { isOpen, modalId, closeModal, openModal } = useModalContext();
  const [id] = useState(crypto.randomUUID());

  if (isOpen == undefined) {
    throw new Error("Contexto de Modal não inicializado!");
  }

  function handleState() {
    isOpen && modalId === id
      ? closeModal()
      : openModal(withControls, component, id);
  }

  return handleState;
}
