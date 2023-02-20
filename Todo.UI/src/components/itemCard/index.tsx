import moment from "moment";
import { Text } from "../../assets/css/global.styles";
import { Item } from "../../types/item";
import { CardContainer, CardFooter, CardGroup, LeftDecoration } from "./styles";
import PriorityIndicator from "../priorityIndicator";

type ItemCardProps = {
  data: Item;
  onClick?: () => void;
};
export default function ItemCard({ data, onClick }: ItemCardProps) {
  return (
    <CardContainer onClick={onClick}>
      <LeftDecoration />
      <CardGroup>
        <Text>{data.title}</Text>
        <PriorityIndicator size={24} priority={data.priority} />
      </CardGroup>
      <CardFooter>
        <Text size="thin" weight={200}>
          Criado em: {moment(data.createdDate).format("DD/MM/YYYY")}
        </Text>
      </CardFooter>
    </CardContainer>
  );
}
