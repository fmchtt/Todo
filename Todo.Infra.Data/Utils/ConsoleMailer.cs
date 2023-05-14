using Todo.Application.Utils;

namespace Todo.Infra.Data.Utils;

public class ConsoleMailer : IMailer
{
    public Task<bool> SendMail(string subject, string body)
    {
        Console.WriteLine($"Titulo: {subject}");
        Console.WriteLine($"Texto: {body}");

        return new Task<bool>(() => true);
    }
}