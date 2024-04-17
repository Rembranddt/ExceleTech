using ExceleTech.Application.DTO;
using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.AccountResponses;
using MediatR;

namespace ExceleTech.Application.Commands.AccountCommands.LoginAccountCommand
{
    public class LoginAccountCommand : IRequest<BaseResponse<LoginResponse>>
    {
        public LoginAccountDTO loginAccountDto { get; set; }
    }
}