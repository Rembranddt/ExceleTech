namespace ExceleTech.Application.Responses.AccountResponses
{
    public class LoginResponse
    {
        public Guid UserId { get; set; }

        public string RefreshToken { get; set; }

        public string AccessToken { get; set; }
    }
}