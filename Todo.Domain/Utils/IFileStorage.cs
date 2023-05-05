using Todo.Domain.DTO;

namespace Todo.Domain.Utils;

public interface IFileStorage
{
    public Task<string> SaveFileAsync(FileDTO file);
}