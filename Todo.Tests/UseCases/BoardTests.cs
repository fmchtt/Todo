using Todo.Domain.DTO.Input;
using Todo.Domain.UseCases;
using Todo.Tests.Repositories;

namespace Todo.Tests.UseCases;

public class BoardTests
{
    private readonly FakeBoardRepository boardRepository = new FakeBoardRepository();

    [Fact]
    public void ShouldCreateBoard()
    {
        var data = new CreateBoardDTO("Teste", String.Empty);
        var result = new CreateBoardUseCase(boardRepository).Handle(data, FakeBoardRepository.testUser);

        Assert.Equal(201, result.Code);

        var board = boardRepository.GetById(result.Result.Id);

        Assert.NotNull(board);
        Assert.Equal("Teste", board.Name);
    }

    [Fact]
    public void ShouldEditBoard()
    {
        var data = new EditBoardDTO("Nome novo", null);
        var result = new EditBoardUseCase(boardRepository).Handle(data, Guid.Parse("5bc63d83-a6e4-4ad4-a9f5-9f253ac6101d"), FakeBoardRepository.testUser);

        Assert.Equal(200, result.Code);

        var board = boardRepository.GetById(result.Result.Id);

        Assert.NotNull(board);
        Assert.Equal("Nome novo", board.Name);
    }

    [Fact]
    public void ShouldntEditBoard()
    {
        var data = new EditBoardDTO("Nome novo", null);
        var result = new EditBoardUseCase(boardRepository).Handle(data, Guid.Parse("5bc63d83-a6e4-4ad4-a9f5-9f253ac6101d"), FakeBoardRepository.OtherUser);

        Assert.Equal(401, result.Code);
    }

    [Fact]
    public void ShouldntFindBoardToEdit()
    {
        var data = new EditBoardDTO("Nome novo", null);
        var result = new EditBoardUseCase(boardRepository).Handle(data, Guid.NewGuid(), FakeBoardRepository.OtherUser);

        Assert.Equal(404, result.Code);
    }

    [Fact]
    public void ShouldDeleteBoard()
    {
        var result = new DeleteBoardUseCase(boardRepository).Handle(Guid.Parse("5bc63d83-a6e4-4ad4-a9f5-9f253ac6101d"), FakeBoardRepository.testUser);

        Assert.Equal(200, result.Code);

        var board = boardRepository.GetById(Guid.Parse("5bc63d83-a6e4-4ad4-a9f5-9f253ac6101d"));

        Assert.Null(board);
    }

    [Fact]
    public void ShouldntDeleteBoard()
    {
        boardRepository.Boards[1].Participants.Add(FakeBoardRepository.OtherUser);
        var result = new DeleteBoardUseCase(boardRepository).Handle(Guid.Parse("5bc63d83-a6e4-4ad4-a9f5-9f253ac6101d"), FakeBoardRepository.OtherUser);

        Assert.Equal(401, result.Code);
    }

    [Fact]
    public void ShouldntFindBoardToDelete()
    {
        var result = new DeleteBoardUseCase(boardRepository).Handle(Guid.NewGuid(), FakeBoardRepository.testUser);

        Assert.Equal(404, result.Code);
    }
}
