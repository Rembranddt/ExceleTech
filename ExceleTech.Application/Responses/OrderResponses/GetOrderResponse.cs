using ExceleTech.Application.DTO;
using ExceleTech.Domain.Enums;

namespace ExceleTech.Application.Responses.OrderResponses
{
    public class GetOrderResponse
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderLineItemDTO> LineItems { get; set; }
    }
}
