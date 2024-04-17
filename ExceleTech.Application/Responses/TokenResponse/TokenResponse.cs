namespace ExceleTech.Application.Responses.TokenResponse
{
    public record TokenResponse 
    {
        public string RefreshToken { get; set; }

        public string AccessToken { get; set; }
    }
}