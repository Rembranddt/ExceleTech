using ExceleTech.Domain.Primitives;

namespace ExceleTech.Domain.Entities
{
    public class Basket : AggregateRoot<Guid>
    {
        public Guid UserId { get; private set; }
        public Guid BasketLineItemId { get; private set; }
        private List<BasketLineItem> _lineItems = new List<BasketLineItem>();
        public IReadOnlyList<BasketLineItem> LineItems => _lineItems ;

        private Basket(Guid UserId) 
        {
            Id = Guid.NewGuid();
            this.UserId = UserId;
        }

        public static Basket Create(Guid UserId)
        {
            return new Basket(UserId);
        }
        
        public BasketLineItem SetQuantityToLineItem(int quantity, Guid LineItemId)
        {
            BasketLineItem LineItem = _lineItems.FirstOrDefault(li => li.Id == LineItemId);


            if (LineItem == null) return null;
            if (quantity <= 0)
            {
                DeleteLineItem(LineItemId);
                return null;
            };
            LineItem.Quantity = quantity;
            return LineItem;
        }
        public BasketLineItem AddLineItem(int quantity,Product product) 
        { 
           if (quantity <= 0 || product == null ) return null;
           BasketLineItem lineItem = new BasketLineItem(quantity,product,Id);
            _lineItems.Add(lineItem);
            return lineItem;
        }
        public BasketLineItem DeleteLineItem(Guid LineItemId) 
        {          
            BasketLineItem lineItem = _lineItems.FirstOrDefault(li => li.Id == LineItemId);
            if (lineItem == null) return null;
            _lineItems.Remove(lineItem);
            return lineItem;
        }
        internal Order CreateOrder()
        {
           
            if (_lineItems.Count == 0) return null;

            Order order = new Order(UserId);
            foreach (BasketLineItem lineItem in _lineItems) 
            {
                order.AddLineItem(lineItem.Product,lineItem.Quantity);
            }
            _lineItems.Clear();
            return order;
        }
    }
}
