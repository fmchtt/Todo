import User from "@/types/user";

export type Comment = {
  id: string;
  author: User;
  text: string;
  creationTimeStamp: string;
  updateTimeStamp: string;
};

export type CreateComment = {
  itemId: string;
  text: string;
};

export type EditComment = {
  id: string;
  itemId: string;
  text: string;
};

export type DeleteComment = {
  id: string;
  itemId: string;
};
