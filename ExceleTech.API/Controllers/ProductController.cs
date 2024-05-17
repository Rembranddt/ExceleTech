using ExceleTech.Application.Commands.ProductCommands;
using ExceleTech.Application.DTO;
using ExceleTech.Application.Queries.ProductQueries;
using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.ProductResponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExceleTech.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController
    {
        private readonly ISender _mediator;
        public ProductController(ISender mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Создание продукта
        /// </summary>
        /// <param name="createProductDTO"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<BaseResponse<CreateProductResponse>> Create(CreateProductDTO createProductDTO)
        {
            CreateProductCommand command = new CreateProductCommand(createProductDTO);

            return await _mediator.Send(command);
        }

        /// <summary>
        /// Поиск продукта
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [HttpGet("Products")]
        public async Task<BaseResponse<SearchProductsResponse>> SearchProducts([FromQuery] SearchProductsDTO dto, int page, int PageSize)
        {
            GetProductsQuery command = new GetProductsQuery()
            {
                SearchProductsDTO = dto,
                page = page,
                pageSize = PageSize
            };

            return await _mediator.Send(command);
        }
    }
}
