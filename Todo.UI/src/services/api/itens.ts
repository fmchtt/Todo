import http from "../http";

interface ItensProps {
  title: string;
  description: string;
  creator: {
    name: string;
    avatarUrl: string;
  };
  files: string[];
  done: boolean;
  priority: number;
  boardId: string;
  columnId: string;
}

export async function getItens() {
  const { data } = await http.get("/itens");
  return data;
}
