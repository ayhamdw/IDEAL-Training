using System.Net;
using System.Net.Mail;
using DataEntity.Models;
using ProjectBase.Services.IServices;
using ProjectBase.Core;

namespace ProjectBase.Services.Services;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(string email, string subject, string body)
    {
        using var client = new SmtpClient(Constants.SystemSettings.EmailsSmtpClient)
        {
            Port = Convert.ToInt32(Constants.SystemSettings.EmailsSmtpPort),
            Credentials = new NetworkCredential(Constants.SystemSettings.EmailsSourceEmail, Constants.SystemSettings.EmailsPassword),
            EnableSsl = true,
        };

        using var mailMessage = new MailMessage
        {
            From = new MailAddress(Constants.SystemSettings.EmailsSourceEmail, Constants.SystemSettings.EmailSenderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(email);

        try
        {
            await client.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
            throw;
        }
    }
}