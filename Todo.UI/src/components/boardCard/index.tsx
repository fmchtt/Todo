import { useNavigate } from "react-router-dom";
import {
  CardContainer,
  CardHeading,
  CardBody,
  CardTitle,
  CardSubtitle,
  DataContainer,
  DataGroup,
} from "./styles";
import { ResumedBoard } from "../../types/board";
import { Text } from "../../assets/css/global.styles";

type BoardCardProps = {
  data: ResumedBoard;
};
export default function BoardCard({ data }: BoardCardProps) {
  const navigate = useNavigate();

  return (
    <CardContainer role="button" onClick={() => navigate(`/board/${data.id}`)}>
      <CardHeading>
        <CardTitle>{data.name}</CardTitle>
        <CardSubtitle>{data.description}</CardSubtitle>
      </CardHeading>
      <CardBody>
        <DataContainer>
          <DataGroup>
            <CardSubtitle>Concluídos</CardSubtitle>
            <Text>
              {data.doneItemCount} / {data.itemCount}
            </Text>
          </DataGroup>
          <DataGroup>
            <CardSubtitle>Abertos</CardSubtitle>
            <Text>
              {data.itemCount - data.doneItemCount} / {data.itemCount}
            </Text>
          </DataGroup>
        </DataContainer>
      </CardBody>
    </CardContainer>
  );
}
