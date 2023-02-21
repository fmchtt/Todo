import moment from "moment";
import { Text } from "@/assets/css/global.styles";
import { Item } from "@/types/item";
import { CardContainer, CardFooter, CardGroup, LeftDecoration } from "./styles";
import PriorityIndicator from "../priorityIndicator";

type ItemCardProps = {
  data: Item;
  draggable?: boolean;
  onClick?: () => void;
  onDragStart?: (id: string) => void;
};
export default function ItemCard({
  data,
  onClick,
  draggable,
  onDragStart,
}: ItemCardProps) {
  return (
    <CardContainer
      id={data.id}
      onDragStart={() => (onDragStart ? onDragStart(data.id) : null)}
      draggable={draggable}
      onClick={onClick}
    >
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
