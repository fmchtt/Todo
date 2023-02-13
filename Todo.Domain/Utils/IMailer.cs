namespace Todo.Domain.Utils;

public interface IMailer
{
    public Task<bool> SendMail(string subject, string body);
}
