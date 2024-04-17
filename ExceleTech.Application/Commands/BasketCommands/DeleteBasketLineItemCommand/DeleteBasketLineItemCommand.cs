using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.BasketResponses;
using MediatR;

namespace ExceleTech.Application.Commands.BasketCommands.DeleteBasketLineItemCommand
{
    public class DeleteBasketLineItemCommand : IRequest<BaseResponse<DeleteBasketLineItemResponse>>
    {
       
        public Guid UserId { get; set; }
        public Guid LineItemId { get; set; }

    }
}
