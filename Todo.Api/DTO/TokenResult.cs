namespace Todo.Api.DTO;

public class TokenResult
{
    public string Token { get; set; }

    public TokenResult(string token)
    {
        Token = token;
    }
}
