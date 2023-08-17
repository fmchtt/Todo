using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Todo.Application.Utils;
using Todo.Infra.Data.Configurations;

namespace Todo.Infra.Data.Utils;

public class SESMailer : IMailer
{
    private readonly EmailConfigurations _configuration;
    private readonly SmtpClient _smtp;

    public SESMailer(IOptions<EmailConfigurations> options)
    {
        _configuration = options.Value;
        _smtp = new SmtpClient
        {
            Host = _configuration.Host,
            Port = _configuration.Port,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Credentials = new NetworkCredential(_configuration.UserName, _configuration.UserPassword),
            Timeout = 8000
        };
    }

    public async Task<bool> SendMail(string subject, string body, string recipient)
    {
        var sender = new MailAddress(_configuration.SenderMail, _configuration.SenderName);
        var recipientMail = new MailAddress(recipient);

        var message = new MailMessage(sender, recipientMail)
        {
            Subject = subject,
            Body = body,
        };
        
        try
        {
            await _smtp.SendMailAsync(message);
        }
        catch (Exception e)
        {
            Console.Write(e);
            return false;
        }

        return true;
    }
}