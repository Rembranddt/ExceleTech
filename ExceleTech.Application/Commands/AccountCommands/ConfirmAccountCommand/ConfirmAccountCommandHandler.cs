using ExceleTech.Application.Resources;
using ExceleTech.Application.Responses;
using ExceleTech.Domain.Interfaces.Services;
using MediatR;
using System.Net;

namespace ExceleTech.Application.Commands.AccountCommands.ConfirmAccountCommand
{
    public class ConfirmAccountCommandHandler : IRequestHandler<ConfirmAccountCommand,BaseResponse>
    {
        private readonly ICacheService<string> _cacheService;

        public ConfirmAccountCommandHandler(ICacheService<string> cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<BaseResponse> Handle(ConfirmAccountCommand request, CancellationToken cancellationToken)
        {
            var result = await _cacheService.GetDataAsync(request.ConfirmAccountDTO.UserId.ToString());
            BaseResponse response = new BaseResponse();

            if (result != request.ConfirmAccountDTO.code)
            {
                response.AddError(ErrorMessage.InternalServerError);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }
            
            return response;
        }
    }
}