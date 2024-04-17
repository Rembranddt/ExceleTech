using System.Security.Claims;
using System.Text;
using ExceleTech.Domain.Interfaces.Services;
using ExceleTech.Domain.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace ExceleTech.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _secretKey;

        private readonly string _issuer;

        private readonly string _audience;

        private readonly double _lifeTime;

        public TokenService(IOptions<TokenOptions> options)
        {
            _secretKey = options.Value.SecretKey;
            _issuer = options.Value.Issuer;
            _audience = options.Value.Audience;
            _lifeTime = options.Value.LifeTime;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(_issuer, _audience, claims, null, DateTime.UtcNow.AddMinutes(_lifeTime), credentials);
            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }

        public string GenerateRefreshToken()
        {
            byte[] randomNumbers = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumbers);
            return Convert.ToBase64String(randomNumbers);
        }
        
        public bool IsValidToken(string accessToken)
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);

            if (jwtToken == null)
                return false;

            if (jwtToken.ValidTo < DateTime.UtcNow)
                return false;

            return true;
        }
    }
}