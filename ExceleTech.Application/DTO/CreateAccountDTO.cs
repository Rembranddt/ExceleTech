namespace ExceleTech.Application.DTO
{
    public sealed class CreateAccountDTO
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
    }
}