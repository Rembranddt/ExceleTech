using ExceleTech.Domain.Entities;

namespace ExceleTech.Domain.Interfaces.Repositories
{
    public interface IBasketRepository
    {
        Task<Basket> GetBasketByUserIdAsync(Guid UserId);
        Task<Basket> GetBasketByIdAsync(Guid Id);
        Basket Update(Basket basket);
        void AddBasket(Basket basket);
    }
}
