using ExceleTech.Domain.Common;
using ExceleTech.Domain.Enums;

namespace ExceleTech.Application.Responses.ProductResponses
{
    public class CreateProductResponse
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public ProductCategory Category { get;  set; }
        public Sku Sku { get; set; }
        public Money Price { get; set; }
    }
}
