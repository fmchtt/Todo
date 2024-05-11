import { H1, Text } from "@/assets/css/global.styles";
import { EditItem, ExpandedItem, getPriorityDisplay } from "@/types/item";
import {
  PresentationBody,
  PresentationContainer,
  PresentationDataGroup,
  PresentationGroup,
  PresentationSide,
} from "./styles";
import moment from "moment";
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
import RoundedAvatar from "@/components/roundedAvatar";
import { toast } from "react-toastify";
import CommentSection from "@/components/commentSection";
import Description from "./components/description";
import {
  useItemDelete,
  useItemDone,
  useItemUpdate,
} from "@/adapters/itemAdapters";

type ItemPresentationProps = {
  data: ExpandedItem;
  boardId?: string;
  onCloseClick: () => void;
  boards?: { label: string; value: string }[];
};

export default function ItemPresentation({
  data,
  onCloseClick,
}: ItemPresentationProps) {
  const navigate = useNavigate();

  const doneMutation = useItemDone();
  const editMutation = useItemUpdate({
    onSuccess: () => {
      toast.success("Item editado com sucesso!");
    },
    onError: () => {
      toast.error("Item não editado, tente novamente mais tarde!");
    },
  });
  const deleteMutation = useItemDelete({
    onSuccess: () => {
      toast.success("Tarefa deletada com sucesso!");
      onCloseClick();
    },
    onError: () => {
      toast.error("Item não deletado, tente novamente mais tarde!");
    },
  });

  const [confirmationModal, openConfirmation] = useConfirmationModal({
    message: `Tem certeza que deseja apagar a tarefa ${data.title} ?`,
    onConfirm: handleDeleteItem,
  });

  function handleDeleteItem() {
    deleteMutation.mutate(data.id);
  }

  async function handleDone(done: boolean) {
    doneMutation.mutate({ id: data.id, done });
  }

  async function handleEdit(values: Omit<EditItem, "id">) {
    editMutation.mutate({ id: data.id, ...values });
  }

  return (
    <PresentationContainer>
      {confirmationModal}
      <PresentationBody>
        <PresentationGroup $flex>
          <H1>{data.title}</H1>
          <PriorityIndicator $size={26} $priority={data.priority} />
        </PresentationGroup>
        <Description
          description={data.description}
          onChange={(value) => {
            handleEdit({ description: value });
          }}
        />
        <CommentSection itemId={data.id} />
      </PresentationBody>
      <PresentationSide>
        <PresentationGroup $flex>
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
          <PresentationDataGroup $padding="10px">
            <TbCalendarEvent size={24} />
            <Text>{moment(data.createdDate).format("DD/MM/YYYY")}</Text>
          </PresentationDataGroup>
        </PresentationGroup>
        <PresentationGroup>
          <Text>Prioridade:</Text>
          <PresentationDataGroup $padding="10px">
            <PriorityIndicator $size={24} $priority={data.priority} />
            <Text>{getPriorityDisplay(data.priority)}</Text>
          </PresentationDataGroup>
        </PresentationGroup>
        {data.board !== undefined && (
          <PresentationGroup>
            <Text>Quadro:</Text>
            <PresentationDataGroup
              $padding="10px"
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
