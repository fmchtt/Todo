using Todo.Domain.DTO;
using Todo.Domain.DTO.Input;
using Todo.Domain.DTO.Output;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.UseCases;

public class EditColumnUseCase
{
    private readonly IColumnRepository _columnRepository;

    public EditColumnUseCase(IColumnRepository columnRepository) { 
        _columnRepository = columnRepository; 
    }

    public ResultDTO<ColumnResultDTO> Handle(EditColumnDTO data, Guid columnId, User user)
    {
        var column = _columnRepository.GetById(columnId);
        if (column == null)
        {
            return new ResultDTO<ColumnResultDTO>(404, "Coluna não encontrada!");
        }

        if (column.Board.Participants.Find(x => x.Id == user.Id) == null)
        {
            return new ResultDTO<ColumnResultDTO>(401, "Sem permissão para apagar a coluna!");
        }

        if (data.Name != null && column.Name != data.Name)
        {
            column.Name = data.Name;
        }

        if (data.Order != null && column.Order != data.Order)
        {
            column.Order = data.Order.Value;
            _columnRepository.ColumnReorder(column.BoardId, column.Id, data.Order.Value);
        }

        _columnRepository.Update(column);

        return new ResultDTO<ColumnResultDTO>(200, "Coluna alterada com sucesso!", new ColumnResultDTO(column));
    }
}
