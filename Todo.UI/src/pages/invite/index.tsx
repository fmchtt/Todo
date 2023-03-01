import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Container } from "./styles";
import Header from "@/components/header";
import useAuth from "@/context/auth";
import { createParticipant } from "@/services/api/boards";
import Spinner from "@/components/spinner";
import { AxiosError } from "axios";
import ErrorMessage from "@/components/forms/ErrorMessage";

export default function Invite() {
  const { id } = useParams();
  const { user, isLoading } = useAuth();
  const navigate = useNavigate();
  const [message, setMessage] = useState();

  useEffect(() => {
    if (!user && !isLoading) {
      return navigate("/login?next=" + window.location.pathname);
    } else if (user) {
      handleParticipant();
    }
  }, [user, isLoading]);

  async function handleParticipant() {
    if (id) {
      try {
        await createParticipant(id);
        navigate(`/board/${id}`);
      } catch (e) {
        if (e instanceof AxiosError) {
          setMessage(e.response?.data.message || "Erro!");
        } else {
          console.log(e);
        }
      }
    }
  }

  return (
    <>
      <Header />
      <Container>
        {message ? (
          <ErrorMessage>{message}</ErrorMessage>
        ) : (
          <Spinner size="50px" />
        )}
      </Container>
    </>
  );
}
