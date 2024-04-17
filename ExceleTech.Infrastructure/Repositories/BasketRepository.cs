using ExceleTech.Domain.Entities;
using ExceleTech.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExceleTech.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly EFContext _context;

        public BasketRepository(EFContext context)
        {
            _context = context;
        }

        public void AddBasket(Basket basket)
        {
            var a = _context.Baskets.Add(basket);
        }

        public async Task<Basket> GetBasketByIdAsync(Guid Id)
        {
            return await GetAggregate().FirstOrDefaultAsync(b => b.Id == Id);
        }

        public async Task<Basket> GetBasketByUserIdAsync(Guid UserId)
        {

            return await GetAggregate().FirstOrDefaultAsync(b => b.UserId == UserId);

        }

        public Basket Update(Basket basket)
        {

            _context.Update(basket);

            return basket;
        }


        private IQueryable<Basket> GetAggregate()
        {
            return _context.Baskets.Include(b => b.LineItems).ThenInclude(li => li.Product);
        }
    }
}
