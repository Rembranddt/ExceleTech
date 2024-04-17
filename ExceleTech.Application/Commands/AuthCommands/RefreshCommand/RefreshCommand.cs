using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.TokenResponse;
using ExceleTech.Domain.DTO;
using MediatR;

namespace ExceleTech.Application.Commands.AuthCommands.RefreshCommand
{
    public class RefreshCommand : IRequest<BaseResponse<TokenResponse>>
    {
        public TokenDTO tokenDTO { get; set; }
    }
}