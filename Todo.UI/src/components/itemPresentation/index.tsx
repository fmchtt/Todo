import { H1, Text } from "@/assets/css/global.styles";
import { EditItem, getPriorityDisplay } from "@/types/item";
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
  useItem,
  useItemDelete,
  useItemDone,
  useItemUpdate,
} from "@/adapters/itemAdapters";
import Spinner from "../spinner";

type ItemPresentationProps = {
  id: string;
  boardId?: string;
  onCloseClick: () => void;
  boards?: { label: string; value: string }[];
};

export default function ItemPresentation({
  id,
  onCloseClick,
}: ItemPresentationProps) {
  const navigate = useNavigate();

  const { data, isLoading } = useItem(id);

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
    message: `Tem certeza que deseja apagar a tarefa ${data?.title} ?`,
    onConfirm: handleDeleteItem,
  });

  function handleDeleteItem() {
    if (!data) return;
    deleteMutation.mutate(data.id);
  }

  async function handleDone(done: boolean) {
    if (!data) return;
    doneMutation.mutate({ id: data.id, done });
  }

  async function handleEdit(values: Omit<EditItem, "id">) {
    if (!data) return;
    editMutation.mutate({ id: data.id, ...values });
  }

  if (isLoading || !data) {
    return <Spinner />;
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
