using CoreTier.Configs;
using CoreTier.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System.Text;
using System.Threading.Tasks;

namespace CoreTier.Services
{
    public class EmailSevice : IEmailService
    {

        private EmailServiceConfig _options;

        public void Configure(EmailServiceConfig options) 
        {
            _options = options;
        }

        public async Task SendEmailAsync(string emailAddres, string emailSubject, string emailBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Администрация сайта", _options.Address));
            message.To.Add(new MailboxAddress("", emailAddres));
            message.Subject = emailSubject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailBody
            };
            using (var client = new SmtpClient())
            {
                client.Connect(_options.SMTP,  _options.SMTPPort, true);
                client.Authenticate(_options.Login, _options.Password);
                client.Send(message);
                client.Disconnect(true);
            }

        }
    }
}
