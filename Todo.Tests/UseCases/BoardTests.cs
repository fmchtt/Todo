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
        var data = new CreateBoardDTO("Teste");
        var result = new CreateBoardUseCase(boardRepository).Handle(data, FakeBoardRepository.testUser);

        Assert.Equal(201, result.Code);

        var boards = boardRepository.GetAllByName("Teste", FakeBoardRepository.testUser.Id);

        Assert.NotNull(boards);
        Assert.True(boards.Count > 0);
    }

    [Fact]
    public void ShouldEditBoard()
    {
        var data = new EditBoardDTO("Nome novo");
        var result = new EditBoardUseCase(boardRepository).Handle(data, Guid.Parse("5bc63d83-a6e4-4ad4-a9f5-9f253ac6101d"), FakeBoardRepository.testUser);

        Assert.Equal(200, result.Code);

        var boards = boardRepository.GetAllByName("Nome novo", FakeBoardRepository.testUser.Id);

        Assert.NotNull(boards);
        Assert.True(boards.Count > 0);
    }

    [Fact]
    public void ShouldntEditBoard()
    {
        var data = new EditBoardDTO("Nome novo");
        var result = new EditBoardUseCase(boardRepository).Handle(data, Guid.Parse("5bc63d83-a6e4-4ad4-a9f5-9f253ac6101d"), FakeBoardRepository.OtherUser);

        Assert.Equal(401, result.Code);
    }

    [Fact]
    public void ShouldntFindBoardToEdit()
    {
        var data = new EditBoardDTO("Nome novo");
        var result = new EditBoardUseCase(boardRepository).Handle(data, Guid.NewGuid(), FakeBoardRepository.OtherUser);

        Assert.Equal(404, result.Code);
    }

    [Fact]
    public void ShouldDeleteBoard()
    {
        var result = new DeleteBoardUseCase(boardRepository).Handle(Guid.Parse("5bc63d83-a6e4-4ad4-a9f5-9f253ac6101d"), FakeBoardRepository.testUser);

        Assert.Equal(200, result.Code);

        var boards = boardRepository.GetAllByName("Nome novo", FakeBoardRepository.testUser.Id);

        Assert.NotNull(boards);
        Assert.True(boards.Count < 1);
    }

    [Fact]
    public void ShouldntDeleteBoard()
    {
        // Not participant
        var result = new DeleteBoardUseCase(boardRepository).Handle(Guid.Parse("5bc63d83-a6e4-4ad4-a9f5-9f253ac6101d"), FakeBoardRepository.OtherUser);

        Assert.Equal(401, result.Code);

        // Participant but not Owner
        boardRepository.Boards[1].Participants.Add(FakeBoardRepository.OtherUser);
        result = new DeleteBoardUseCase(boardRepository).Handle(Guid.Parse("5bc63d83-a6e4-4ad4-a9f5-9f253ac6101d"), FakeBoardRepository.OtherUser);

        Assert.Equal(401, result.Code);
    }

    [Fact]
    public void ShouldntFindBoardToDelete()
    {
        var result = new DeleteBoardUseCase(boardRepository).Handle(Guid.NewGuid(), FakeBoardRepository.testUser);

        Assert.Equal(404, result.Code);
    }
}
