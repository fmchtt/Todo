using Todo.Domain.DTO;

namespace Todo.Application.Utils;

public interface IFileStorage
{
    public Task<string> SaveFileAsync(FileDTO file);
}