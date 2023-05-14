using Todo.Application.DTO;

namespace Todo.Application.Utils;

public interface IFileStorage
{
    public Task<string> SaveFileAsync(UploadedFile file);
}