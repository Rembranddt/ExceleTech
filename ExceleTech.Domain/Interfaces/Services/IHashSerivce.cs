using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceleTech.Domain.Interfaces.Services
{
    public interface IHashService
    {
        public string HashPassword(string password);
        public bool IsPasswordCorrect(string password, string PasswordHash);
    }
}
