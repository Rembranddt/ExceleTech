using ExceleTech.Application.DTO;
using ExceleTech.Application.Responses;
using MediatR;

namespace ExceleTech.Application.Commands.AccountCommands.ConfirmAccountCommand
{
    public class ConfirmAccountCommand : IRequest<BaseResponse>
    {
        public ConfirmAccountDTO ConfirmAccountDTO{ get; set; }
    }
}