namespace ExceleTech.Application.DTO
{
    public record CreateAccountDTO(string Name, string password, string confirmPassword, string email);
}