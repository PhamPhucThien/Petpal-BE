using CapstoneProject.Business;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Email;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public new StatusCode StatusCode { get; set; } = new();
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
    
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail(SendEmailRequest request)
        {
            try
            {
                _emailService.SendEmail(request);
                var response = new ResponseObject<String>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Send email successfully";
                response.Payload.Data = null;
                return Ok(response);
            }
            catch (FormatException)
            {
                return Unauthorized(new ResponseObject<string>()
                {
                    Payload = new Payload<string>("", "Bạn chưa đăng nhập"),
                    Status = StatusCode.Unauthorized
                });
            }
            catch (Exception)
            {
                return BadRequest(new ResponseObject<string>()
                {
                    Payload = new Payload<string>("", "Lỗi hệ thống"),
                    Status = StatusCode.BadRequest
                });
            }
        }
    }
}
