using ExceleTech.Application.DTO;
using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.AccountResponses;
using MediatR;

namespace ExceleTech.Application.Commands.AccountCommands.CreateAccountCommand
{
    public class CreateAccountCommand : IRequest<BaseResponse<CreateAccountResponse>>
    {
        public CreateAccountDTO CreateAccountDTO { get; init; }
    }
}