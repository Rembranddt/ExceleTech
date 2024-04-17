
using ExceleTech.Domain.Enums;
using ExceleTech.Domain.Primitives;
using System.Text.Json.Serialization;

namespace ExceleTech.Domain.Entities
{
    public class Order : AggregateRoot<Guid>
    {
        private const int DefaultDeliveryTime = 7;
        private readonly List<OrderLineItem> _lineItems = new();
        public IReadOnlyList<OrderLineItem> LineItems => new List<OrderLineItem>(_lineItems);
        public Guid UserId { get; private set; }

        public OrderStatus Status { get; private set; }

        public DateTime OrderedDate { get; private set; }
        public DateTime? ExpectedDeliveryDate { get; private set; }
        public DateTime? DeliveredDate { get; private set; }
        public void AddLineItem(Product product, int quantity)
        {
            if (product == null || quantity <= 0) return;
            _lineItems.Add(new OrderLineItem(product, quantity,Id));
        }
        public void SetQuantityToLineItem(int quantity,Guid LineItemId)
        {
            OrderLineItem LineItem = _lineItems.FirstOrDefault(li => li.Id == LineItemId);
           
            if (LineItem == null) return;
            if (quantity <= 0)
            {
                DeleteLineItem(LineItemId);
                return;
            };
            LineItem.Quantity = quantity;
        }
        public void DeleteLineItem(Guid LineItemId)
        {
            OrderLineItem lineItem = _lineItems.FirstOrDefault(l =>  l.Id == LineItemId);
            if (lineItem != null) 
            _lineItems.Remove(lineItem);
        }
        
        
        public static Order Create(Basket basket)
        {
            Order order = basket.CreateOrder();
            return order;
        }
        public Order Deliver()
        {
            Status = OrderStatus.Delivered;
            DeliveredDate = DateTimeOffset.Now.DateTime;
            return this;    
        }
        public Order Cancel()
        {
            Status = OrderStatus.Cancelled;
            ExpectedDeliveryDate = null; 
            return this;
        }
        internal Order(Guid UserId)
        {
            Id = Guid.NewGuid();
                 
            this.UserId = UserId;
            OrderedDate = DateTimeOffset.Now.DateTime;
            ExpectedDeliveryDate = DateTimeOffset.Now.DateTime.AddDays(DefaultDeliveryTime).Date;
            Status = OrderStatus.Delivering;
        }
        

        [JsonConstructor]
        private Order() 
        { 

        }
    }
}
