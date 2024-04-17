using System.Security.Claims;
using ExceleTech.Application.Commands.BasketCommands.AddBasketLineItemCommand;
using ExceleTech.Application.Commands.BasketCommands.DeleteBasketLineItemCommand;
using ExceleTech.Application.DTO;
using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.BasketResponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExceleTech.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BasketController : ControllerBase
{
    private readonly IMediator _mediator;
    public BasketController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Добавление предмета в корзину
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("AddLineItem")]
    public async Task<BaseResponse<AddBasketLineItemResponse>> AddLineItem(AddBasketLineItemDTO dto)
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

        var command = new AddBasketLineItemCommand(dto, new Guid(userId));

        return await _mediator.Send(command);

    }

    /// <summary>
    /// Удаление предмета из корзины
    /// </summary>
    /// <param name="BasketId"></param>
    /// <param name="LineItemId"></param>
    /// <returns></returns>
    [HttpDelete("DeleteLineItem")]
    public async Task<BaseResponse<DeleteBasketLineItemResponse>> DeleteLineItem(Guid BasketId, Guid LineItemId)
    {
        var command = new DeleteBasketLineItemCommand()
        {
            UserId = BasketId,
            LineItemId = LineItemId
        };

        return await _mediator.Send(command);
    }

}

