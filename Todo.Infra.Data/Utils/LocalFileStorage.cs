using Todo.Application.DTO;
using Todo.Application.Utils;

namespace Todo.Infra.Data.Utils;

public class LocalFileStorage : IFileStorage
{
    public async Task<string> SaveFileAsync(UploadedFile file)
    {
        var datetime = DateTime.Now;
        var timestamp = $"{datetime.Day}-{datetime.Month}-{datetime.Year}";
        var basePath = Path.Join(Environment.CurrentDirectory, "wwwroot", "uploads", timestamp);

        if (!Directory.Exists(basePath))
        {
            Directory.CreateDirectory(basePath);
        }
        var filename = $"{Guid.NewGuid()}-{file.Name}";

        var fullPath = Path.Join(basePath, filename);

        await using (var stream = File.Create(fullPath))
        {
            await file.Content.CopyToAsync(stream);
        }

        return Path.Join("uploads", timestamp, filename).Replace("\\", "/");
    }
}
