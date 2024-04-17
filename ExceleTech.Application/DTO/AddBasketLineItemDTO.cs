namespace ExceleTech.Application.DTO
{
    public sealed class AddBasketLineItemDTO
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
