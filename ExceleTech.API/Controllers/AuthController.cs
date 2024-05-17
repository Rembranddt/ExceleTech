using ExceleTech.Application.Commands.AuthCommands.RefreshCommand;
using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.TokenResponse;
using ExceleTech.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExceleTech.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController
    {
        private readonly ISender _mediator;

        public AuthController(ISender mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Обновление токена
        /// </summary>
        /// <param name="tokenDTO"></param>
        /// <returns></returns>
        [HttpPut("/refresh")]
        public async Task<BaseResponse<TokenResponse>> Refresh(TokenDTO tokenDTO)
        {
            RefreshCommand command = new RefreshCommand
            {
                tokenDTO = tokenDTO
            };

            var response = await _mediator.Send(command);

            return response;
        }
    }
}