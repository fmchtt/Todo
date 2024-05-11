import { AxiosError } from "axios";

export type MutationAdapter<T> = {
  onSuccess?: (values: T) => void;
  onError?: (e: AxiosError) => void;
};
