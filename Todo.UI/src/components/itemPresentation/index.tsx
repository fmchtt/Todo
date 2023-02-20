import { H1, Text } from "../../assets/css/global.styles";
import { Item, getPriorityDisplay } from "../../types/item";
import { RoundedAvatar } from "../header/styles";
import {
  PresentationBody,
  PresentationContainer,
  PresentationDataGroup,
  PresentationGroup,
  PresentationSide,
} from "./styles";
import moment from "moment";
import { deleteItem } from "../../services/api/itens";
import { useQueryClient } from "react-query";
import PriorityIndicator from "../priorityIndicator";
import { TbCalendarEvent, TbTrash, TbX } from "react-icons/tb";

type ItemPresentationProps = {
  data: Item;
  onCloseClick: () => void;
};

export default function ItemPresentation({
  data,
  onCloseClick,
}: ItemPresentationProps) {
  const client = useQueryClient();

  function handleDeleteItem() {
    deleteItem(data.id).then((res) => {
      client.invalidateQueries(["itens"]);
      onCloseClick();
    });
  }

  return (
    <PresentationContainer>
      <PresentationBody>
        <PresentationGroup flex={true}>
          <H1>{data.title}</H1>
          <PriorityIndicator size={26} priority={data.priority} />
        </PresentationGroup>
        <Text>{data.description}</Text>
      </PresentationBody>
      <PresentationSide>
        <PresentationGroup flex={true}>
          <TbTrash
            role="button"
            size={26}
            cursor="pointer"
            onClick={handleDeleteItem}
          />
          <TbX
            role="button"
            size={26}
            cursor="pointer"
            onClick={onCloseClick}
          />
        </PresentationGroup>
        <PresentationGroup>
          <Text>Criador:</Text>
          <PresentationDataGroup>
            {data.creator.avatarUrl && (
              <RoundedAvatar
                src={import.meta.env.VITE_API_URL + data.creator.avatarUrl}
              />
            )}
            <Text>{data.creator.name}</Text>
          </PresentationDataGroup>
        </PresentationGroup>
        <PresentationGroup>
          <Text>Criado em:</Text>
          <PresentationDataGroup padding="10px">
            <TbCalendarEvent size={24} />
            <Text>{moment(data.createdDate).format("DD/MM/YYYY")}</Text>
          </PresentationDataGroup>
        </PresentationGroup>
        <PresentationGroup>
          <Text>Prioridade:</Text>
          <PresentationDataGroup padding="10px">
            <PriorityIndicator size={24} priority={data.priority} />
            <Text>{getPriorityDisplay(data.priority)}</Text>
          </PresentationDataGroup>
        </PresentationGroup>
      </PresentationSide>
    </PresentationContainer>
  );
}
