using ExceleTech.Domain.Entities;

namespace ExceleTech.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<Dictionary<Guid, int>> GetStatisticAboutOrderedProductsAsync();
        Order Add(Order order);
        Task<List<Order>> GetOrdersByUserIdAsync(Guid userId);
        Task<List<Order>> GetOrdersByUserIdNoTrackingAsync(Guid userId);
        Task<Order> GetOrderByIdAsync(Guid OrderId);
    }
}
