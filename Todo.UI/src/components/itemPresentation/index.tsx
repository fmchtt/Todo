import { H1, Text } from "@/assets/css/global.styles";
import { ExpandedItem, getPriorityDisplay } from "@/types/item";
import {
  PresentationBody,
  PresentationContainer,
  PresentationDataGroup,
  PresentationGroup,
  PresentationSide,
} from "./styles";
import moment from "moment";
import { changeDone, deleteItem } from "@/services/api/itens";
import { useQueryClient } from "@tanstack/react-query";
import PriorityIndicator from "../priorityIndicator";
import {
  TbCalendarEvent,
  TbCheck,
  TbCircleOff,
  TbLayoutKanban,
  TbTrash,
  TbX,
} from "react-icons/tb";
import { useNavigate } from "react-router-dom";
import useConfirmationModal from "@/hooks/useConfirmationModal";
import { ExpandedBoard, ResumedBoard } from "@/types/board";
import RoundedAvatar from "@/components/roundedAvatar";

type ItemPresentationProps = {
  data: ExpandedItem;
  boardId?: string;
  onCloseClick: () => void;
  boards?: { label: string; value: string }[];
};

export default function ItemPresentation({
  data,
  onCloseClick,
  boardId,
}: ItemPresentationProps) {
  const navigate = useNavigate();
  const client = useQueryClient();

  const [confirmationModal, openConfirmation] = useConfirmationModal({
    message: `Tem certeza que deseja apagar a tarefa ${data.title} ?`,
    onConfirm: handleDeleteItem,
  });

  function handleDeleteItem() {
    deleteItem(data.id).then(() => {
      client.invalidateQueries(["itens"]);
      client.invalidateQueries(["board"]);
      onCloseClick();
    });
  }

  async function handleDone(done: boolean) {
    await changeDone(data.id, done);

    if (data.board || boardId) {
      try {
        client.setQueryData<ExpandedBoard>(
          ["board", data.board?.id || boardId],
          (prev) => {
            if (!prev) {
              throw new Error("Quadro inexistente");
            }

            const colIdx = prev.columns.findIndex(
              (x) => x.itens.findIndex((x) => x.id === data.id) !== -1
            );
            const itemIdx = prev.columns[colIdx].itens.findIndex(
              (x) => x.id === data.id
            );

            prev.columns[colIdx].itens[itemIdx].done = done;
            if (done) {
              prev.doneItemCount += 1;
            } else {
              prev.doneItemCount -= 1;
            }

            return prev;
          }
        );
      } catch (e) {
        console.log(e);
      }

      client.setQueryData<ResumedBoard[]>(["boards"], (prev) => {
        if (!prev) {
          throw new Error("Cache invalido!");
        }

        const boardIdx = prev.findIndex(
          (x) => x.id === (data.board?.id || boardId)
        );
        if (boardIdx >= 0) {
          if (done) {
            prev[boardIdx].doneItemCount += 1;
          } else {
            prev[boardIdx].doneItemCount -= 1;
          }
        }

        return prev;
      });
    }

    client.setQueryData<ExpandedItem[]>(["itens"], (prev) => {
      if (!prev) {
        throw new Error("Cache invalido!");
      }
      const itemIdx = prev.findIndex((x) => x.id === data.id);
      if (itemIdx >= 0) {
        prev[itemIdx].done = done;
      }

      return prev;
    });
  }

  return (
    <PresentationContainer>
      {confirmationModal}
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
            onClick={openConfirmation}
          />
          {data.done ? (
            <TbCircleOff
              role="button"
              size={26}
              cursor="pointer"
              onClick={() => handleDone(false)}
            />
          ) : (
            <TbCheck
              role="button"
              size={26}
              cursor="pointer"
              onClick={() => handleDone(true)}
            />
          )}
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
            <RoundedAvatar
              avatarUrl={
                data.creator.avatarUrl &&
                import.meta.env.VITE_API_URL + data.creator.avatarUrl
              }
              name={data.creator.name}
            />
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
        {data.board !== undefined && (
          <PresentationGroup>
            <Text>Quadro:</Text>
            <PresentationDataGroup
              padding="10px"
              onClick={() =>
                data.board ? navigate(`/board/${data.board?.id}`) : null
              }
            >
              <TbLayoutKanban size={24} />
              <Text>{data.board?.name || "Sem quadro"}</Text>
            </PresentationDataGroup>
          </PresentationGroup>
        )}
      </PresentationSide>
    </PresentationContainer>
  );
}
