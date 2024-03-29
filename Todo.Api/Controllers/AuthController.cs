﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Application.Commands.UserCommands;
using Todo.Application.Queries;
using Todo.Application.Results;

namespace Todo.Api.Controllers;

[ApiController, Route("auth")]
public class AuthController : TodoBaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AuthController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpGet("me"), Authorize]
    [ProducesResponseType(typeof(ResumedUserResult), 200)]
    public ResumedUserResult Me()
    {
        var user = GetUser();
        return _mapper.Map<ResumedUserResult>(user);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(TokenResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public async Task<TokenResult> Login(
        LoginCommand command
    )
    {
        var result = await _mediator.Send(command);

        return result;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(TokenResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public async Task<TokenResult> Register(
        [FromBody] RegisterCommand command
    )
    {
        var result = await _mediator.Send(command);

        return result;
    }

    [HttpPost("password/reset")]
    [ProducesResponseType(typeof(MessageResult), 200)]
    public async Task<MessageResult> PasswordReset(
        RecoverPasswordCommand command
    )
    {
        await _mediator.Send(command);

        return new MessageResult("Caso o email esteja registrado, um código será enviado ao email");
    }

    [HttpPost("password/reset/verify")]
    [ProducesResponseType(typeof(MessageResult), 200)]
    [ProducesResponseType(typeof(MessageResult), 404)]
    public async Task<MessageResult> VerifyCode(
        [FromBody] GetConfirmationCodeQuery data
    )
    {
        var response = await _mediator.Send(data);
        
        return new MessageResult(response);
    }


    [HttpPost("password/reset/confirm")]
    [ProducesResponseType(typeof(MessageResult), 200)]
    public async Task<MessageResult> PasswordResetConfirm(
        ConfirmRecoverPasswordCommand command
    )
    {
        var result = await _mediator.Send(command);

        return new MessageResult(result);
    }
}