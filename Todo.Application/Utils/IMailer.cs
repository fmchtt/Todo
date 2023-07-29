namespace Todo.Application.Utils;

public interface IMailer
{
    public Task<bool> SendMail(string subject, string body, string recipient);
}
