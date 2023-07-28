import { Helmet } from "react-helmet";
import RegisterForm from "@/components/forms/RegisterForm";
import SideFormContainer from "@/components/sideFormContainer";

export default function Register() {
  return (
    <SideFormContainer>
      <Helmet>
        <title>Registrar - Taskerizer</title>
      </Helmet>
      <RegisterForm />
    </SideFormContainer>
  );
}
