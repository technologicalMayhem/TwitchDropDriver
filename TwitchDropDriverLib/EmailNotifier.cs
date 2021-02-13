using System.Net.Mail;
using System.Threading.Tasks;

namespace TwitchDropDriverLib
{
    public class EmailNotifier
    {
        private readonly EmailConfiguration _configuration;
        private readonly SmtpClient _client;

        public EmailNotifier(EmailConfiguration configuration)
        {
            _configuration = configuration;
            _client = new SmtpClient(configuration.SmtpAddress, configuration.SmtpPort);
        }

        public async Task Notify(string subject, string message)
        {
            await _client.SendMailAsync(new MailMessage("example@example.com", _configuration.To, subject, message));
        }
    }
}