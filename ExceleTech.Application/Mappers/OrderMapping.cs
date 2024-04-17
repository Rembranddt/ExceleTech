using AutoMapper;
using ExceleTech.Application.DTO;
using ExceleTech.Application.Responses.OrderResponses;
using ExceleTech.Domain.Entities;

namespace ExceleTech.Application.Mappers
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<Order, OrderResponse>()
                .ForMember(resp => resp.OrderId, opt => opt.MapFrom(or => or.Id))
                .ForMember(resp => resp.LineItems, opt => opt.MapFrom(or => or.LineItems.Select(li => new OrderLineItemDTO()
                {
                    ProductId = li.ProductId,
                    ProductPrice = li.ProductPrice,
                    Quantity = li.Quantity,
                }).ToList()));

            CreateMap<Order, GetOrderResponse>()
                .ForMember(resp => resp.OrderId, opt => opt.MapFrom(or => or.Id))
                .ForMember(resp => resp.LineItems, opt => opt.MapFrom(or => or.LineItems.Select(li => new OrderLineItemDTO()
                {
                    ProductId = li.ProductId,
                    ProductPrice = li.ProductPrice,
                    Quantity = li.Quantity,
                }).ToList()));
        }
    }
}


