namespace Todo.Application.DTO;

public class UploadedFile
{
    public Stream Content { get; set; }
    public string Name { get; set; }
    public string ContentType { get; set; }

    public UploadedFile(Stream content, string name, string contentType)
    {
        Content = content;
        Name = name;
        ContentType = contentType;
    }
}