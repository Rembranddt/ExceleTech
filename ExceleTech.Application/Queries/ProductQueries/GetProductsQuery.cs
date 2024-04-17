using ExceleTech.Application.DTO;
using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.ProductResponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceleTech.Application.Queries.ProductQueries
{
    public class GetProductsQuery : IRequest<BaseResponse<SearchProductsResponse>>
    {
        public SearchProductsDTO SearchProductsDTO { get; set; }
        public int page { get; set; }   
        public int pageSize { get; set; }

    }
}
