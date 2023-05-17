using MediatR;

namespace Todo.Application.Queries;

public class GetConfirmationCodeQuery : IRequest<string>
{
    public int Code { get; set; }
    public string Email { get; set; }

    public GetConfirmationCodeQuery(string email, int code)
    {
        Email = email;
        Code = code;
    }
}