using Todo.Domain.Utils;

namespace Todo.Infra.Utils;

public class EmailMailer : IMailer
{
    public async Task<bool> SendMail(string subject, string body)
    {
        Console.WriteLine($"Titulo: {subject}");
        Console.WriteLine($"Texto: {body}");

        return true;
    }
}
