import moment from "moment";
import { Text } from "@/assets/css/global.styles";
import { ResumedItem } from "@/types/item";
import { CardContainer, CardFooter, CardGroup, LeftDecoration } from "./styles";
import PriorityIndicator from "../priorityIndicator";
import { DetailedHTMLProps } from "react";

type DivProps = DetailedHTMLProps<
  React.HTMLAttributes<HTMLDivElement>,
  HTMLDivElement
>;
type ItemCardProps = DivProps & {
  data: ResumedItem;
  draggable?: boolean;
  onClick?: () => void;
};
export default function ItemCard({ data, onClick, ...props }: ItemCardProps) {
  return (
    <CardContainer {...props} id={data.id} onClick={onClick}>
      <LeftDecoration />
      <CardGroup>
        <Text $lineLimiter $dashed={data.done}>
          {data.title}
        </Text>
        <PriorityIndicator $size={24} $priority={data.priority} />
      </CardGroup>
      <CardFooter>
        <Text $size="thin" $weight={200}>
          Criado em: {moment(data.createdDate).format("DD/MM/YYYY")}
        </Text>
      </CardFooter>
    </CardContainer>
  );
}
