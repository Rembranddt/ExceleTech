namespace ExceleTech.Domain.ValueObjects
{
    public record Token
    {       
        public string RefreshToken { get; private set; }
        public DateTime RefreshTokenExpiryTime { get; private set; }
        internal Token(string refreshToken,DateTime refreshTokenExpiryTime)
        {
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = refreshTokenExpiryTime;
        }
    }
}
