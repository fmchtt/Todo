import { Container, FormSide } from "./styles";
import { Side, ContentTextSide } from "./styles";
import image from "@/assets/images/image-side.png";
import { H1, H2, Text } from "@/assets/css/global.styles";
import { PropsWithChildren } from "react";

export default function SideFormContainer(props: PropsWithChildren) {
  return (
    <Container>
      <Side>
        <ContentTextSide>
          <H1>Olá,</H1>
          <H2>Bem-vindos ao Taskerizer</H2>
          <Text>
            Aqui você pode gerenciar seu dia da melhor forma possivel!
          </Text>
        </ContentTextSide>
        <img src={image} />
      </Side>
      <FormSide>{props.children}</FormSide>
    </Container>
  );
}
