using ExceleTech.Domain.Common;
using ExceleTech.Domain.Enums;

namespace ExceleTech.Application.DTO
{
    public sealed class ProductDTO
    {
        public Guid ProductId { get; init; }
        public string Name { get; init; }
        public ProductCategory Category { get; init; }
        public int PercentDiscount { get; init; }
        public Sku Sku { get; init; }
        public Money Price { get; init; }
        public int TimesOrdered {  get; init; }
    }
}
