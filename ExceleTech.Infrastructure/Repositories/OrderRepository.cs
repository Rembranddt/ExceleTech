using ExceleTech.Domain.Entities;
using ExceleTech.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExceleTech.Infrastructure.Repositories
{
    public class ProductStat
    {
        public int TimesOrdered { get; set; }
    }
    public class OrderRepository : IOrderRepository
    {
        private readonly EFContext  _context;
        public async Task<Dictionary<Guid,int>> GetStatisticAboutOrderedProductsAsync() 
        {
            Dictionary<Guid, int> Statistic = await GetAggregate()
                .SelectMany(x => x.LineItems)
                .GroupBy(x => x.ProductId)
                .ToDictionaryAsync
                (
                 gr => gr.Key,
                 gr => gr.Sum(s => s.Quantity)
                ) ;
            

            return Statistic;
                                            
        }
        private IQueryable<Order> GetAggregate()
        {
            return _context.Orders.Include(o => o.LineItems);
        }

        public OrderRepository(EFContext context)
        {
            _context = context;
        }

        public Order Add(Order order)
        {
            _context.Orders.Add(order);
            return order;
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(Guid userId)
        {
            return await GetAggregate().Where(o => o.UserId == userId).ToListAsync();
             
        }

        public async Task<List<Order>> GetOrdersByUserIdNoTrackingAsync(Guid userId)
        {
            return await GetAggregate().Where(o => o.UserId == userId).AsNoTracking().OrderByDescending(o => o.Status).ToListAsync();
            
        }

        public async Task<Order> GetOrderByIdAsync(Guid OrderId)
        {
            return await GetAggregate().OrderByDescending(o => o.Status).FirstOrDefaultAsync(b => b.Id == OrderId);
        }
    }
}
