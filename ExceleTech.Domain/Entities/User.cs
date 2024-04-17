using ExceleTech.Domain.Enums;
using ExceleTech.Domain.Primitives;
using ExceleTech.Domain.ValueObjects;

namespace ExceleTech.Domain.Entities
{
    public class User : AggregateRoot<Guid>
    {
        private User(string Name, string Email, string password)
        {
            Id = Guid.NewGuid();
            this.Name = Name;
            this.Email = Email;
            this.Password = password;
        }

        public Guid? BasketId { get; private set; }
        public string Role { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public bool EmailVerified { get; private set; }
        public Token? UserToken { get; private set; }
        public void AddNewToken(string token, DateTime expiryTime)
        {
            if (token is null) return;
            UserToken = new Token(token, expiryTime);
        }
        public void SetRole(UserRole role)
        {
            Role = role.ToString();
        }
        public static User Create(string Name, string Email, string password)
        {
            User user = new User(Name, Email, password);
            user.SetRole(UserRole.User);
            return user;
        }
        public void Verify()
        {
            EmailVerified = true;
        }
    }
}
