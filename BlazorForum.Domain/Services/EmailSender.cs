using BlazorForum.Domain.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace BlazorForum.Domain.Services
{
    public class EmailSender : IEmailSender
    {
        private IManageConfiguration _config;
        private Models.Configuration _configData;
        public EmailSender(IManageConfiguration config)
        {
            _config = config;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            _configData = _config.GetConfig();
            return Execute(_configData.SendGridKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_configData.EmailAddress, _configData.EmailSenderName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}
