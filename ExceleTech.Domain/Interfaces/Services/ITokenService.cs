using System.Security.Claims;

namespace ExceleTech.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);

        bool IsValidToken(string accessToken);

        string GenerateRefreshToken();


    }
}