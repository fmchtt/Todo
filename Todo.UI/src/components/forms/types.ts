import { EditBoard } from "@/types/board";

export interface FormBoardProps {
  maxWidth?: string;
  borderRadius?: string;
  closeModal?: () => void;
  data?: EditBoard;
}
