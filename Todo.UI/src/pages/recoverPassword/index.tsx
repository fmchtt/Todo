import ResetPasswordForm from "@/components/forms/ResetPasswordForm";
import SideFormContainer from "@/components/sideFormContainer";
import { Helmet } from "react-helmet";

export default function RecoverPassword() {
  return (
    <SideFormContainer>
      <Helmet>
        <title>Recuperar senha - Taskerizer</title>
      </Helmet>
      <ResetPasswordForm />
    </SideFormContainer>
  );
}
