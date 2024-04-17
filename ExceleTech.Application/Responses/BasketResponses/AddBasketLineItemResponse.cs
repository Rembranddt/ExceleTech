using ExceleTech.Domain.Common;

namespace ExceleTech.Application.Responses.BasketResponses
{
    public class AddBasketLineItemResponse
    {
        public Guid BasketLineItemId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public Sku Sku { get; set; }
        public Money UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
