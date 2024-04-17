using ExceleTech.Domain.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

namespace ExceleTech.Application.Services
{
    public class HashService : IHashService
    {
        private readonly int SaltLenght = 9;
        public string HashPassword(string password)
        {
            string salt = GetSalt();
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password + salt));
            return BitConverter.ToString(bytes) + salt;
        }
        private string GetSalt() 
        {
            Random rand = new Random();
            string salt = string.Empty;
            string sourse = "ABCDEFGHPOIDC1234567890";
            for (int i = 0; salt.Length < SaltLenght; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    salt += sourse[rand.Next(0, sourse.Length)];
                }
                salt += "-";
            }
            return salt;

        }
        public bool IsPasswordCorrect(string password,string PasswordHash)
        {
            string SaltFromHash = PasswordHash[^SaltLenght..];
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes((password + SaltFromHash)));
            string NewHash = BitConverter.ToString(bytes) + SaltFromHash; 
            if (NewHash == PasswordHash)
            {
                return true;
            }
            return false;
        }
    }
}
