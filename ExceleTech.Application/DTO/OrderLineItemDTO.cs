using ExceleTech.Domain.Common;

namespace ExceleTech.Application.DTO
{
    public sealed class OrderLineItemDTO
    {
        public Guid ProductId { get;set; }
        public int Quantity { get; set; }
        public Money ProductPrice { get; set; }
    }
}
