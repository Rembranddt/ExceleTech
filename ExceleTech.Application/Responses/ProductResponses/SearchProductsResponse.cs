using ExceleTech.Application.DTO;

namespace ExceleTech.Application.Responses.ProductResponses
{
    public class SearchProductsResponse
    {
        public IEnumerable<ProductDTO> Products { get; set; }
    }
}
