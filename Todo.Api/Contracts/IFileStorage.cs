namespace Todo.Api.Contracts;

public interface IFileStorage
{
    public Task<string> SaveFile(IFormFile file);
}
