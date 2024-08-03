namespace CapstoneProject.DTO.Request.Email;

public class SendEmailRequest
{ 
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}