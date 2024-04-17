using AutoMapper;
using ExceleTech.Application.DTO;
using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.ProductResponses;
using ExceleTech.Domain.Entities;
using ExceleTech.Domain.Interfaces.Repositories;
using ExceleTech.Domain.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using ExceleTech.Application.Extensions;
using System.Threading.Tasks;

namespace ExceleTech.Application.Queries.ProductQueries
{
    internal class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, BaseResponse<SearchProductsResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICacheService<SearchProductsResponse> _cache;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(
            IProductRepository productRepository, 
            ICacheService<SearchProductsResponse> cache, 
            IMapper mapper)
        {
            _productRepository = productRepository;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<BaseResponse<SearchProductsResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = $"produts-{request.SearchProductsDTO.SearchString}-{request.SearchProductsDTO.Category}";
            SearchProductsResponse Data = await _cache.GetDataAsync(cacheKey);
            var response = new BaseResponse<SearchProductsResponse>();
            if (Data != null )
            {
                Data.Products = Data.Products.ApplySorting(request.SearchProductsDTO.Criteria,request.SearchProductsDTO.IsAscending)
                                             .UsePagination(request.page,request.pageSize);
                response.AddData(Data);
                return response;
            }
            
            List<Product> products = await _productRepository.SearchProductsNoTrackingAsync(request.SearchProductsDTO);
            if ( products.Count == 0)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.AddError("ПРАДУКТАВ НЕТ");
                return response;
            }

            Data = _mapper.Map<SearchProductsResponse>(products);
            await _cache.SetDataAsync(cacheKey, Data, TimeSpan.FromSeconds(20));

            Data.Products = Data.Products.UsePagination(request.page, request.pageSize);
            response.AddData(Data);
            return response;
            
        }
    }
}
