using ExceleTech.Application.DTO;
using ExceleTech.Domain.Entities;

namespace ExceleTech.Domain.Interfaces.Repositories
{
    public interface IProductRepository 
    {
        Task<List<Product>> GetAllProductForThisBasketAsync(Guid BasketId);

        Task<Product> GetProductByNameAsync(string Name);
        Task<Product> GetProductBySkuValueAsync(string sku);
        Task<List<Product>> SearchProductsNoTrackingAsync(SearchProductsDTO dto);
        Task<Product> GetProductByIdNoTrackingAsync(Guid productId);
        Task<Product> GetProductByIdAsync(Guid productId);
        Task UpdateProductStatisticsAsync(Dictionary<Guid, int> data);
        Product Add(Product product);

    }
}
