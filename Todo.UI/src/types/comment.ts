import User from "@/types/user";

export type Comment = {
  id: string;
  author: User;
  text: string;
  creationTimeStamp: string;
  updateTimeStamp: string;
};
