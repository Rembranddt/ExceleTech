using ExceleTech.Domain.Enums;

namespace ExceleTech.Domain.ValueObjects
{
    public record Role
    {
        public string RoleName { get; private set; }

        internal Role(UserRole role)
        {
            RoleName = role.ToString();
        }
        public Role(string role) 
        { 
            RoleName = role;
        }
    }
}
