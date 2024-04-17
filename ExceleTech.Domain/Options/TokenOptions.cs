namespace ExceleTech.Domain.Options
{
    public class TokenOptions
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecretKey { get; set; }

        public double LifeTime { get; set; }

        public double RefreshLifeTime { get; set; }
    }
}