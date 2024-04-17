﻿using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.OrderResponses;
using MediatR;

namespace ExceleTech.Application.Commands.OrderCommands.CancelOrderCommand
{
    public class CancelOrderCommand : IRequest<BaseResponse<OrderResponse>>
    {
        public Guid OrderId { get; init; }
    }
}
