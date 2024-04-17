using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.OrderResponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceleTech.Application.Commands.OrderCommands.CreateOrderCommand
{
    public class CreateOrderCommand : IRequest<BaseResponse<OrderResponse>>
    {
        public CreateOrderCommand(Guid basketId)
        {
            UserId = basketId;
        }

        public Guid UserId { get; init; }
    }
}
