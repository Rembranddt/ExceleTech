using ExceleTech.Application.DTO;
using ExceleTech.Application.Responses;
using MediatR;

namespace ExceleTech.Application.Commands.AccountCommands.SetRoleCommand
{
    public record SetRoleCommand (SetRoleDTO SetRoleDTO) : IRequest<BaseResponse>;
}
