using AutoMapper;
using ExceleTech.Application.Resources;
using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.OrderResponses;
using ExceleTech.Domain.Entities;
using ExceleTech.Domain.Interfaces.Repositories;
using MediatR;
using System.Net;
using System.Transactions;

namespace ExceleTech.Application.Commands.OrderCommands.CreateOrderCommand
{
    internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, BaseResponse<OrderResponse>>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(
            IBasketRepository basketRepository, 
            IOrderRepository orderRepository, 
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _basketRepository = basketRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<OrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _unitOfWork.BeginTransaction(IsolationLevel.RepeatableRead);
                Basket basket = await _basketRepository.GetBasketByUserIdAsync(request.UserId);
                BaseResponse<OrderResponse> response = new BaseResponse<OrderResponse>();
                if (basket == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.AddError(ErrorMessage.BasketNotFound);
                    return response;
                }
                Order order = Order.Create(basket);
                if (order == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.AddError(ErrorMessage.LiteItemNotFound);
                    return response;
                }
                _orderRepository.Add(order);
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.CommitTransaction();
                response.AddData(_mapper.Map<OrderResponse>(order));
                return response;
            }
            catch(Exception)
            {
                _unitOfWork.RollBackTransaction();
                throw;
            }





        }
    }
}
