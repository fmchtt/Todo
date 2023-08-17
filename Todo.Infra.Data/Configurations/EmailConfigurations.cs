namespace Todo.Infra.Data.Configurations;

public class EmailConfigurations
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string SenderMail { get; set; } = string.Empty;
    public string SenderName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string UserPassword { get; set; } = string.Empty;
}