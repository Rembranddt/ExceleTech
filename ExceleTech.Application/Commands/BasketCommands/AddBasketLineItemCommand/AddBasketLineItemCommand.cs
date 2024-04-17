using ExceleTech.Application.DTO;
using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.BasketResponses;
using MediatR;

namespace ExceleTech.Application.Commands.BasketCommands.AddBasketLineItemCommand;

public class AddBasketLineItemCommand : IRequest<BaseResponse<AddBasketLineItemResponse>>
{
    public AddBasketLineItemCommand(AddBasketLineItemDTO addLineItemDTO, Guid userId)
    {
        AddLineItemDTO = addLineItemDTO;
        UserId = userId;
    }

    public Guid UserId { get; set; }

    public AddBasketLineItemDTO AddLineItemDTO { get; set; }
}

