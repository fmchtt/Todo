import userService from "@/services/userService";
import { useQuery } from "@tanstack/react-query";

type UseUserProps = {
  enabled?: boolean;
};
export function useUser(props?: UseUserProps) {
  return useQuery({
    queryKey: ["me"],
    queryFn: userService.getActualUser,
    enabled: props?.enabled,
  });
}
