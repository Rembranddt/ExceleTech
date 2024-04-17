using ExceleTech.Domain.Common;
using ExceleTech.Domain.Primitives;
using System.Text.Json.Serialization;

namespace ExceleTech.Domain.Entities
{
    public class OrderLineItem : Entity<Guid>
    {
        internal OrderLineItem(Product product, int quantity, Guid orderId) 
        {
            Id = Guid.NewGuid();    
            ProductId = product.Id;
            OrderId = orderId;
            Quantity = quantity;
            ProductPrice = (product.Price * (100 - product.PercentDiscount)) / 100 ;
        }
        
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; internal set; }
        public Money ProductPrice { get; private set; }
        [JsonConstructor]
        private OrderLineItem() { }
        
    }
}
