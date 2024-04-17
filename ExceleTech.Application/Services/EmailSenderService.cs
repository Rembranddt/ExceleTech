using ExceleTech.Domain.Interfaces.Services;
using ExceleTech.Domain.Options;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;

namespace ExceleTech.Application.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IOptions<EmailOptions> _emailOptions;

        private readonly ICacheService<string> _cacheService;

        public EmailSenderService(IOptions<EmailOptions> emailOptions, ICacheService<string> cacheService)
        {
            _emailOptions = emailOptions;
            _cacheService = cacheService;
        }

        public async Task<bool> SendAsync(string emailAddress, string message, Guid userId)
        {
                using (var mailMessage = new MimeMessage())
                {
                    mailMessage.From.Add(MailboxAddress.Parse(_emailOptions.Value.SenderEmailAdress));
                    mailMessage.To.Add(MailboxAddress.Parse(emailAddress));
                    mailMessage.Subject = "Confirm code";
                    mailMessage.Body = new TextPart(TextFormat.Html)
                    {
                        Text = message
                    };
           
                    using (SmtpClient client = new SmtpClient())
                    {
                        await client.ConnectAsync(_emailOptions.Value.Server, _emailOptions.Value.Port, true);
                        await client.AuthenticateAsync(_emailOptions.Value.SenderEmailAdress, _emailOptions.Value.SenderEmailPassword);
                        await client.SendAsync(mailMessage);

                        await client.DisconnectAsync(true);
                    }
                }

                await _cacheService.SetDataAsync(userId.ToString(), message, TimeSpan.FromMinutes(5));

                string a = await _cacheService.GetDataAsync(userId.ToString());
                await Console.Out.WriteLineAsync(a);


                return true;
        }
    }
}