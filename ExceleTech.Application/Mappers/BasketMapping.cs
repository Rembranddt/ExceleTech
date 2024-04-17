using AutoMapper;
using ExceleTech.Application.Responses.BasketResponses;
using ExceleTech.Domain.Entities;

namespace ExceleTech.Application.Mappers
{
    public class BasketMapping : Profile
    {
        public BasketMapping() 
        {
            CreateMap<BasketLineItem, AddBasketLineItemResponse>()
             .ForMember(re => re.ProductName, opt => opt.MapFrom(li => li.Product.Name))
             .ForMember(re => re.ProductId, opt => opt.MapFrom(li => li.Product.Id))
             .ForMember(re => re.BasketLineItemId, opt => opt.MapFrom(li => li.Id))
             .ForMember(re => re.UnitPrice, opt => opt.MapFrom(li => li.Product.Price))
             .ForMember(re => re.Sku, opt => opt.MapFrom(li => li.Product.Sku));

            CreateMap<BasketLineItem, DeleteBasketLineItemResponse>()
             .ForMember(re => re.ProductName, opt => opt.MapFrom(li => li.Product.Name))
             .ForMember(re => re.ProductId, opt => opt.MapFrom(li => li.Product.Id))
             .ForMember(re => re.BasketLineItemId, opt => opt.MapFrom(li => li.Id))
             .ForMember(re => re.UnitPrice, opt => opt.MapFrom(li => li.Product.Price))
             .ForMember(re => re.Sku, opt => opt.MapFrom(li => li.Product.Sku));
        }
    }
}
