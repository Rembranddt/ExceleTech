using ExceleTech.Application.Commands.AccountCommands.ConfirmAccountCommand;
using ExceleTech.Application.Commands.AccountCommands.CreateAccountCommand;
using ExceleTech.Application.Commands.AccountCommands.LoginAccountCommand;
using ExceleTech.Application.Commands.AccountCommands.SetRoleCommand;
using ExceleTech.Application.DTO;
using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.AccountResponses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExceleTech.API.Controllers;

[ApiController]
[Route("[Controller]")]
public class AccountController : ControllerBase
{
    private readonly ISender _mediator;

    public AccountController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создание Аккаунта
    /// </summary>
    /// <param name="createAccountModel"></param>
    /// <returns></returns>

    [HttpPost("/Create")]
    public async Task<BaseResponse<CreateAccountResponse>> Create(CreateAccountDTO createAccountModel)
    {
        CreateAccountCommand command = new CreateAccountCommand
        {
            CreateAccountDTO = createAccountModel
        };

        var result = await _mediator.Send(command);

        return result;
    }

    /// <summary>
    /// Подтверждение электронной почты
    /// </summary>
    /// <param name="ConfirmAccountDTO"></param>
    /// <returns></returns>
    [HttpPatch("/Confirm")]
    public async Task<BaseResponse> Confirm(ConfirmAccountDTO ConfirmAccountDTO)
    {
        ConfirmAccountCommand command = new ConfirmAccountCommand
        {
            ConfirmAccountDTO = ConfirmAccountDTO
        };

        var response = await _mediator.Send(command);

        return response;
    }

    /// <summary>
    /// Добавление роли
    /// </summary>
    /// <param name="SetRoleDTO"></param>
    /// <returns></returns>
    [HttpPost("SetRole")]
    [Authorize("UserPolicy")]
    public async Task<BaseResponse> SetRole(SetRoleDTO SetRoleDTO)
    {
        SetRoleCommand command = new SetRoleCommand(SetRoleDTO);

        var response = await _mediator.Send(command);

        return (response);
    }

    /// <summary>
    /// Вход в аккаунт
    /// </summary>
    /// <param name="LoginDTO"></param>
    /// <returns></returns>
    [HttpPost("Login")]
    public async Task<BaseResponse> Login(LoginAccountDTO LoginDTO)
    {
        LoginAccountCommand command = new LoginAccountCommand
        {
            loginAccountDto = LoginDTO,
        };

        var response = await _mediator.Send(command);

        return (response);
    }
}
