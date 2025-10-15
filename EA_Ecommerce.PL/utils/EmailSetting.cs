using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace EA_Ecommerce.PL.utils
{
    public class EmailSetting : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("eidabdallah971@gmail.com", "ljmz fayc kpvu vxaz")
            };

            return client.SendMailAsync(new MailMessage(from: "eidabdallah971@gmail.com", to: email, subject, htmlMessage)
            {
                IsBodyHtml = true
            });
        }
    }
}
