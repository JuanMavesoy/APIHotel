using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Infraestructure.Services;

namespace Infraestructure.Services
{
    public class EmailService : IEmailService
    {
          public async Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpClient = new SmtpClient
            {
                Host = "smtp.office365.com",
                Port = 587,
                Credentials = new NetworkCredential("PruebaIngresoSamrtTalent@outlook.es", "Prueba123456*"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("PruebaIngresoSamrtTalent@outlook.es", "Prueba API HOTEL"),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
