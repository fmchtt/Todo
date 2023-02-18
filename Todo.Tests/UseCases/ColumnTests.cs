using Todo.Domain.DTO.Input;
using Todo.Domain.UseCases;
using Todo.Tests.Repositories;

namespace Todo.Tests.UseCases;

public class ColumnTests
{
    private readonly FakeColumnRepository columnRepository = new FakeColumnRepository();
    private readonly FakeBoardRepository boardRepository = new FakeBoardRepository();

    [Fact]
    public void ShouldCreateColumn()
    {
        var data = new CreateColumnDTO("Teste", FakeColumnRepository.testBoard.Id);
        var result = new CreateColumnUseCase(columnRepository, boardRepository).Handle(data, FakeColumnRepository.testUser);

        Assert.Equal(201, result.Code);

        var column = columnRepository.GetById(result.Result.Id);

        Assert.NotNull(column);
    }

    [Fact]
    public void ShouldEditColumn()
    {
        var data = new EditColumnDTO("Nome novo");
        var result = new EditColumnUseCase(columnRepository).Handle(data, columnRepository.Columns[0].Id, FakeColumnRepository.testUser);

        Assert.Equal(200, result.Code);

        var column = columnRepository.GetById(result.Result.Id);

        Assert.NotNull(column);
        Assert.Equal("Nome novo", column.Name);
    }

    [Fact]
    public void ShouldntEditColumn()
    {
        var data = new EditColumnDTO("Nome novo");
        var result = new EditColumnUseCase(columnRepository).Handle(data, columnRepository.Columns[0].Id, FakeColumnRepository.OtherUser);

        Assert.Equal(401, result.Code);
    }

    [Fact]
    public void ShouldntFindColumnToEdit()
    {
        var data = new EditColumnDTO("Nome novo");
        var result = new EditColumnUseCase(columnRepository).Handle(data, Guid.NewGuid(), FakeColumnRepository.OtherUser);

        Assert.Equal(404, result.Code);
    }

    [Fact]
    public void ShouldDeleteColumn()
    {
        var result = new DeleteColumnUseCase(columnRepository).Handle(Guid.Parse("974a4dcb-3cbe-4064-9476-404462798702"), FakeColumnRepository.testUser);

        Assert.Equal(200, result.Code);

        var column = columnRepository.GetById(Guid.Parse("974a4dcb-3cbe-4064-9476-404462798702"));

        Assert.Null(column);
    }

    [Fact]
    public void ShouldntDeleteColumn()
    {
        var result = new DeleteColumnUseCase(columnRepository).Handle(Guid.Parse("974a4dcb-3cbe-4064-9476-404462798702"), FakeColumnRepository.OtherUser);

        Assert.Equal(401, result.Code);
    }

    [Fact]
    public void ShouldntFindBoardToDelete()
    {
        var result = new DeleteColumnUseCase(columnRepository).Handle(Guid.NewGuid(), FakeColumnRepository.testUser);

        Assert.Equal(404, result.Code);
    }
}
