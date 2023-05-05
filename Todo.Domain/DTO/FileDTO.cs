namespace Todo.Domain.DTO;

public class FileDTO
{
    public Stream Content { get; set; }
    public string Name { get; set; }
    public string ContentType { get; set; }

    public FileDTO(Stream content, string name, string contentType)
    {
        Content = content;
        Name = name;
        ContentType = contentType;
    }
}