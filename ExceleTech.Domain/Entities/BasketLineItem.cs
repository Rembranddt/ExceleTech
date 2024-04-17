using ExceleTech.Domain.Primitives;

namespace ExceleTech.Domain.Entities
{
    public class BasketLineItem : Entity<Guid>
    {
        public Guid BasketId { get; private set; }

        public Guid ProductId { get; private set; }
        
        public Product Product { get; private set; }
        public int Quantity { get; internal set; }
        private BasketLineItem() { } 

        internal BasketLineItem(int quantity, Product product, Guid basketId)
        {
            Quantity = quantity;
            Id = Guid.NewGuid();
            Product = product;
            ProductId = product.Id;
            BasketId = basketId;
        }
    }
}
