using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.OrderResponses;
using MediatR;

namespace ExceleTech.Application.Commands.OrderCommands.ConfirmOrderDeliveringCommand
{
    public class ConfirmOrderDeliveringCommand : IRequest<BaseResponse<OrderResponse>> 
    {
        public Guid OrderId { get; init; }
    }
}
