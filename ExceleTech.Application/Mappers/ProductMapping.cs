using AutoMapper;
using ExceleTech.Application.DTO;
using ExceleTech.Application.Responses.ProductResponses;
using ExceleTech.Domain.Entities;

namespace ExceleTech.Application.Mappers;

public class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<Product, CreateProductResponse>()
            .ForMember(cr => cr.ProductName, opt => opt.MapFrom(p => p.Name))
            .ForMember(cr => cr.ProductId, opt => opt.MapFrom(p => p.Id));

        CreateMap<Product, ProductDTO>()
           .ForMember(cr => cr.ProductId, opt => opt.MapFrom(p => p.Id));
        CreateMap<List<Product>, SearchProductsResponse>()
            .ForMember(sr => sr.Products, opt => opt.MapFrom(p => p));
    }
}

