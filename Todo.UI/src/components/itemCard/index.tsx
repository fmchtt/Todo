import moment from "moment";
import { Text } from "../../assets/css/global.styles";
import { Item } from "../../types/item";
import { CardContainer, CardFooter, LeftDecoration } from "./styles";

type ItemCardProps = {
  data: Item;
  onClick?: () => void;
};

export default function ItemCard({ data, onClick }: ItemCardProps) {
  return (
    <CardContainer onClick={onClick}>
      <LeftDecoration />
      <Text>{data.title}</Text>
      <CardFooter>
        <Text size="thin" weight={200}>
          Criado em: {moment(data.createdDate).format("DD/MM/YYYY")}
        </Text>
      </CardFooter>
    </CardContainer>
  );
}
