using ExceleTech.Application.Commands.OrderCommands.CancelOrderCommand;
using ExceleTech.Application.Commands.OrderCommands.ConfirmOrderDeliveringCommand;
using ExceleTech.Application.Commands.OrderCommands.CreateOrderCommand;
using ExceleTech.Application.Queries.OrderQueries;
using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.OrderResponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExceleTech.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Создание заказа
        /// </summary>
        /// <param name="BasketId"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<BaseResponse<OrderResponse>> Create(Guid BasketId) 
        {
            var command = new CreateOrderCommand(BasketId);

            return await _mediator.Send(command);
           
        }

        /// <summary>
        /// Получение заказа
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet("GetUserOrders")]
        public async Task<BaseResponse<List<OrderResponse>>> GetUserOrders(Guid UserId)
        {
            var query = new GetUserOrdersQuery(UserId);

            return await _mediator.Send(query);
        }

        /// <summary>
        /// Подтверждение доставки
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        [HttpPatch("ConfirmOrderDelivering")]
        public async Task<BaseResponse<OrderResponse>> ConfirmOrderDelivering(Guid OrderId)
        {
            var command = new ConfirmOrderDeliveringCommand() { OrderId = OrderId };

            return await _mediator.Send(command);
        }

        /// <summary>
        /// Отмена заказа
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        [HttpPatch("CancelOrder")]
        public async Task<BaseResponse<OrderResponse>> CancelOrder(Guid OrderId)
        {
            var command = new CancelOrderCommand() { OrderId = OrderId };

            return await _mediator.Send(command);
        }
    }
}
