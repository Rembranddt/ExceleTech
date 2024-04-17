using ExceleTech.Domain.Entities;
using ExceleTech.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExceleTech.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EFContext _context;

        public UserRepository(EFContext context)
        {
            _context = context;
        }
        private IQueryable<User> GetAggregate()
        {
            return _context.Users;
        }
        public User Add(User user)
        {
            if (user == null) return null;

            _context.Users.Add(user);

            return user;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await GetAggregate().ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {

            var user = await GetAggregate().FirstOrDefaultAsync(user => user.Id == userId); ;

            return user;
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            if (login == null)
            {
                return null;
            }

            var user = await GetAggregate().FirstOrDefaultAsync(user => user.Name == login);

            return user;
        }

        public async Task<User> GetUserByTokenAsync(string token)
        {
            if (token == null)
            {
                return null;
            }

            var user = await GetAggregate().FirstOrDefaultAsync(user => user.UserToken.RefreshToken == token);

            return user;
        }
    }
}