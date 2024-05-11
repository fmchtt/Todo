import { ExpandedColumn } from "./column";
import User from "./user";

export type CreateBoard = {
  name: string;
  description: string;
};

export type EditBoard = {
  id: string;
  name?: string;
  description?: string;
};

export type ResumedBoard = {
  id: string;
  name: string;
  description: string;
  itemCount: number;
  doneItemCount: number;
};

export type ExpandedBoard = {
  id: string;
  name: string;
  description: string;
  owner: string;
  columns: ExpandedColumn[];
  participants: User[];
  itemCount: number;
  doneItemCount: number;
};

export type ParticipantInvite = {
  emails: string[];
  boardId: string;
};

export type ParticipantRemove = {
  boardId: string;
  participantId: string;
};
