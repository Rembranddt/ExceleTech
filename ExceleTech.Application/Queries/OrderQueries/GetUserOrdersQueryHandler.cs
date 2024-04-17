using AutoMapper;
using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.OrderResponses;
using ExceleTech.Domain.Entities;
using ExceleTech.Domain.Interfaces.Repositories;
using MediatR;

namespace ExceleTech.Application.Queries.OrderQueries
{
    internal class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQuery, BaseResponse<List<OrderResponse>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetUserOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<List<OrderResponse>>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
        {
            List<Order> orders = await _orderRepository.GetOrdersByUserIdNoTrackingAsync(request.UserId);
            var response = new BaseResponse<List<OrderResponse>>();
            response.AddData(_mapper.Map<List<OrderResponse>>(orders));
            
            return response;
        }
    }
}
