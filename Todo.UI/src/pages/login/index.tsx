import LoginForm from "@/components/forms/LoginForm";
import { Helmet } from "react-helmet";
import SideFormContainer from "@/components/sideFormContainer";

export default function Login() {
  return (
    <SideFormContainer>
      <Helmet>
        <title>Login - Taskerizer</title>
      </Helmet>
      <LoginForm />
    </SideFormContainer>
  );
}
