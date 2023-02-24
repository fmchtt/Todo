using Todo.Domain.DTO;
using Todo.Domain.DTO.Input;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Domain.Utils;

namespace Todo.Domain.UseCases.InviteUseCases;

public class InviteUserUseCase
{
    private readonly IInviteRepository _inviteRepository;
    private readonly IBoardRepository _boardRepository;
    private readonly IMailer _mailer;

    public InviteUserUseCase(IInviteRepository inviteRepository, IBoardRepository boardRepository, IMailer mailer)
    {
        _inviteRepository = inviteRepository;
        _boardRepository = boardRepository;
        _mailer = mailer;
    }

    public ResultDTO Handle(InviteDTO data, Guid boardId, User user, string domain)
    {


        var board = _boardRepository.GetById(boardId);
        if (board == null) {
            return new ResultDTO(404, "Quadro não encontrado!");
        }

        if (board.OwnerId != user.Id)
        {
            return new ResultDTO(401, "Sem permissão para convidar usuários!");
        }

        var invites = new List<Invite>();
        foreach (var email in data.Emails)
        {
            invites.Add(new Invite(email, boardId));
            _mailer.SendMail(email, $"Você foi convidado para participar do quadro: {board.Name}, clique <a href='{domain}/invite/{board.Id}'>aqui</a> para participar!");
        }
        _inviteRepository.CreateMany(invites);

        return new ResultDTO(201, "Convites criados com sucesso!");
    }
}
