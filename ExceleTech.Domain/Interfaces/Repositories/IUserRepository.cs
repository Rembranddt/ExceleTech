using ExceleTech.Domain.Entities;

namespace ExceleTech.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        User Add(User user);
        Task<User> GetUserByIdAsync(Guid user);
        Task<User> GetUserByLoginAsync(string Login);
        Task<List<User>> GetAllUsersAsync();

        Task<User> GetUserByTokenAsync(string token);
    }
}