import { useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Container } from "./styles";
import Header from "@/components/header";
import useAuth from "@/context/auth";
import Spinner from "@/components/spinner";
import { useBoardParticipantConfirm } from "@/adapters/boardAdapters";
import { toast } from "react-toastify";
import { MessageResponse } from "@/types/responses/message";

type InviteParams = {
  id: string;
};
export default function Invite() {
  const { id } = useParams<InviteParams>();
  const { user, isLoading } = useAuth();
  const navigate = useNavigate();

  const createParticipantMutation = useBoardParticipantConfirm({
    onSuccess: () => {
      toast.success("Entrada realizada com sucesso!");
      navigate(`/board/${id}`);
    },
    onError: (e) => {
      const response = e.response?.data as MessageResponse | undefined;
      toast.error(
        response?.message ||
          "Oops, ocorreu um erro ao entrar no quadro, tente novamente mais tarde!"
      );
      navigate("/dashboard");
    },
  });

  useEffect(() => {
    if (!user && !isLoading) {
      return navigate("/login?next=" + window.location.pathname);
    } else if (user) {
      handleParticipant();
    }
  }, [user, isLoading]);

  async function handleParticipant() {
    if (!id) return navigate("/dashboard");
    createParticipantMutation.mutate(id);
  }

  return (
    <>
      <Header />
      <Container>
        <Spinner size="50px" />
      </Container>
    </>
  );
}
