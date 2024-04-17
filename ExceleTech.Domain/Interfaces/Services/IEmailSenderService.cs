namespace ExceleTech.Domain.Interfaces.Services
{
    public interface IEmailSenderService
    {
        Task<bool> SendAsync(string emailAddress, string message, Guid userId);
    }
}