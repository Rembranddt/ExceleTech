using ExceleTech.Application.DTO;
using ExceleTech.Domain.Enums;

namespace ExceleTech.Application.Responses.OrderResponses
{
    public class OrderResponse
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderedDate { get; private set; }
        public DateTime? ExpectedDeliveryDate { get; private set; }
        public DateTime? DeliveredDate { get; private set; }
        public OrderStatus Status { get; set; }
        public List<OrderLineItemDTO> LineItems { get; set; }
    }
}
