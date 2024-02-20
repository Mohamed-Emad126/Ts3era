using Ts3era.Dto.EmailsDto;
using MailKit.Security;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Ts3era.Heler;

namespace Ts3era.Services.EmailServices
{
    public class EmailServices : IEmailServices
    {
        private readonly Emails _email; 
        public EmailServices(IOptions<Emails>email)
        {
            this._email = email.Value;
             
        }
        public async Task SendEmail(EmailRequestDto mail)
        {

            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_email.Email);
            email.To.Add(MailboxAddress.Parse(mail.EmailTO));
            email.Subject = mail.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mail.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_email.Host, _email.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_email.Email, _email.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
