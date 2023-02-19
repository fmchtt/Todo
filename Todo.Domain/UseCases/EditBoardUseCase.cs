using Todo.Domain.DTO;
using Todo.Domain.DTO.Input;
using Todo.Domain.DTO.Output;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.UseCases;

public class EditBoardUseCase
{ 
    private readonly IBoardRepository _boardRepository;

    public EditBoardUseCase(IBoardRepository boardRepository)
    {
        _boardRepository = boardRepository;
    }

    public ResultDTO<BoardResultDTO> Handle(EditBoardDTO data, Guid boardId, User user)
    {
        var board = _boardRepository.GetById(boardId);
        if (board == null)
        {
            return new ResultDTO<BoardResultDTO>(404, "Quadro não encontrado!");
        }

        if (board.Participants.Find(x => x.Id == user.Id) == null)
        {
            return new ResultDTO<BoardResultDTO>(401, "Sem permissão para alterar o quadro");
        }

        if (data.Name != null)
        {
            board.Name = data.Name;
        }

        if (data.Description != null)
        {
            board.Description = data.Description;
        }

        _boardRepository.Update(board);

        return new ResultDTO<BoardResultDTO>(200, "Quadro editado com sucesso!", new BoardResultDTO(board));
    }
}
