using Todo.Api.Contracts;

namespace Todo.Api.Utils;

public class LocalFileStorage : IFileStorage
{
    public string BasePath { get; set; }

    public LocalFileStorage(string basePath)
    {
        BasePath = basePath;
    }

    public async Task<string> SaveFile(IFormFile file)
    {
        var datetime = DateTime.Now;
        var timestamp = $"{datetime.Day}-{datetime.Month}-{datetime.Year}";
        var path = Path.Join(BasePath, "uploads", timestamp);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        var filename = $"{Guid.NewGuid()}-{file.FileName}";

        var fullPath = Path.Join(path, filename);

        using (var stream = File.Create(fullPath))
        {
            await file.CopyToAsync(stream);
        }

        return Path.Join("uploads", timestamp, filename).Replace("\\", "/");
    }
}
