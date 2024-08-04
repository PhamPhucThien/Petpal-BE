using System.Net;
using System.Net.Mail;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.DTO.Request.Email;
using Microsoft.Extensions.Options;

namespace CapstoneProject.Business.Services;

public class EmailService : IEmailService
{
    
    private readonly EmailSetting _emailSetting;
    
    public EmailService(IOptions<EmailSetting> emailSetting)
    {
        _emailSetting = emailSetting.Value;
    }
    public void SendEmail(SendEmailRequest request)
    {
        MailMessage message = new MailMessage()
        {
            From = new MailAddress(_emailSetting.RootEmail),
            Subject = request.Subject,
            To = { new MailAddress(request.To) },
            Body = request.Body
        };

        var smtpClient = new SmtpClient(_emailSetting.Host)
        {
            Port = int.Parse(_emailSetting.Port),
            Credentials = new NetworkCredential(_emailSetting.RootEmail, _emailSetting.RootPassword),
            EnableSsl = true,
        };
        smtpClient.Send(message);
    }
}