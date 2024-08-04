using CapstoneProject.DTO.Request.Email;

namespace CapstoneProject.Business.Interfaces;

public interface IEmailService
{
    void SendEmail(SendEmailRequest request);
}