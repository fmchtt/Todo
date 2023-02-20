import { TiTrash } from "react-icons/ti";
import { IoClose } from "react-icons/io5";
import { H1, Text } from "../../assets/css/global.styles";
import { Item } from "../../types/item";
import { RoundedAvatar } from "../header/styles";
import {
  PresentationBody,
  PresentationContainer,
  PresentationDataGroup,
  PresentationGroup,
  PresentationSide,
} from "./styles";
import moment from "moment";

type ItemPresentationProps = {
  data: Item;
  onCloseClick: () => void;
};

export default function ItemPresentation({
  data,
  onCloseClick,
}: ItemPresentationProps) {
  return (
    <PresentationContainer>
      <PresentationBody>
        <H1>{data.title}</H1>
        <Text>{data.description}</Text>
      </PresentationBody>
      <PresentationSide>
        <PresentationGroup flex={true}>
          <TiTrash role="button" size={30} cursor="pointer" />
          <IoClose
            role="button"
            size={30}
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
            <Text>{moment(data.createdDate).format("DD/MM/YYYY")}</Text>
          </PresentationDataGroup>
        </PresentationGroup>
      </PresentationSide>
    </PresentationContainer>
  );
}
