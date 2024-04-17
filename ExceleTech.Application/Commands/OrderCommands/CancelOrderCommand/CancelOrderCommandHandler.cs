using AutoMapper;
using ExceleTech.Application.Resources;
using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.OrderResponses;
using ExceleTech.Domain.Entities;
using ExceleTech.Domain.Enums;
using ExceleTech.Domain.Interfaces.Repositories;
using MediatR;
using System.Net;


namespace ExceleTech.Application.Commands.OrderCommands.CancelOrderCommand
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, BaseResponse<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CancelOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<OrderResponse>> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            Order order = await _orderRepository.GetOrderByIdAsync(request.OrderId);
            var response = new BaseResponse<OrderResponse>();
            
            if (order == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.AddError(ErrorMessage.OrderNotFound);
                return response;
            }

            if (order.Status == OrderStatus.Delivered)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.AddError(ErrorMessage.OrderAlreadyDelivered);
                return response;
            }

            order.Cancel();
            response.AddData(_mapper.Map<OrderResponse>(order));
            
            return response;    
        }
    }
}
