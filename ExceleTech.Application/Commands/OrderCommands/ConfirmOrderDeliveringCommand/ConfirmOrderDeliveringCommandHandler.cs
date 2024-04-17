using AutoMapper;
using ExceleTech.Application.Resources;
using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.OrderResponses;
using ExceleTech.Domain.Entities;
using ExceleTech.Domain.Enums;
using ExceleTech.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExceleTech.Application.Commands.OrderCommands.ConfirmOrderDeliveringCommand
{
    internal class ConfirmOrderDeliveringCommandHandler : IRequestHandler<ConfirmOrderDeliveringCommand, BaseResponse<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConfirmOrderDeliveringCommandHandler(
            IOrderRepository orderRepository, 
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<OrderResponse>> Handle(ConfirmOrderDeliveringCommand request, CancellationToken cancellationToken)
        {
            Order order = await _orderRepository.GetOrderByIdAsync(request.OrderId);
            var response = new BaseResponse<OrderResponse>();
            
            if (order == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.AddError(ErrorMessage.OrderNotFound);
                return response;
            }

            if (order.Status == OrderStatus.Cancelled)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.AddError(ErrorMessage.OrderHasBeenCancelled);
                return response;
            }

            if (order.Status == OrderStatus.Delivered)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.AddError(ErrorMessage.OrderAlreadyDelivered);
                return response;
            }

            order.Deliver();
            response.AddData(_mapper.Map<OrderResponse>(order));
            await _unitOfWork.SaveChangesAsync();

          
            return response;
        }
    }
}
