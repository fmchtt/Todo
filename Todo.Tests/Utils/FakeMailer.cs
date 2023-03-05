using Todo.Domain.Utils;

namespace Todo.Tests.Utils;

public class FakeMailer : IMailer
{
    public async Task<bool> SendMail(string subject, string body)
    {
        return true;
    }
}