using ExceleTech.Application.DTO;
using ExceleTech.Domain.Entities;
using ExceleTech.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using ExceleTech.Application.Extensions;

namespace ExceleTech.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EFContext _context;

        public ProductRepository(EFContext context)
        {
            _context = context;
        }

        public Product Add(Product product)
        {
            if (product == null) return product;
            _context.Products.Add(product);
            return product;
        }
        public async Task UpdateProductStatisticsAsync(Dictionary<Guid,int> data)
        {
            List<Product> products = await GetAggregate().ToListAsync();
            for (int i = 0; i < products.Count; i++)
            {
                products[i].SetOrderedTimes(data[products[i].Id]);
            }
            _context.UpdateRange(products);

           
        }

        public Task<List<Product>> GetAllProductForThisBasketAsync(Guid BasketId)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductByNameAsync(string Name)
        {

            return await GetAggregate().FirstOrDefaultAsync(p => p.Name == Name);
        }

        public async Task<Product> GetProductBySkuValueAsync(string sku)
        {
            return await GetAggregate().FirstOrDefaultAsync(p => p.Sku.Value == sku);
        }

        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            return await GetAggregate().FirstOrDefaultAsync(p => p.Id == productId);
        }

        private IQueryable<Product> GetAggregate()
        {
            return _context.Products;
        }

        public async Task<Product> GetProductByIdNoTrackingAsync(Guid productId)
        {
            return await GetAggregate().AsNoTracking().FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<List<Product>> GetProductsNoTrackingAsync(int page, int pageSize)
        {
            return await GetAggregate().AsNoTracking().Skip((page-1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<List<Product>> SearchProductsNoTrackingAsync(SearchProductsDTO dto)
        {
            IQueryable<Product> products = GetAggregate().AsNoTracking();
            if(dto.Category != null)
            {
                products = products.Where(p => p.Category == dto.Category);
            }
            if(dto.SearchString != null)
            {
                products = products.Where(p => EF.Functions.ILike(p.Name, $"%{dto.SearchString}%"));
            }
            if (dto.Criteria != null)
            {
                products = products.ApplySorting(dto.Criteria, dto.IsAscending);
            }
            List<Product> searchedProducts = await products.ToListAsync();
            return searchedProducts;



        }
    }
}
