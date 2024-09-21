using CapstoneProject.Business;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.Business.Services;
using CapstoneProject.DTO;
using CapstoneProject.Infrastructure.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController(IDashboardService dashboardService) : ControllerBase
    {
        private readonly IDashboardService _dashboardService = dashboardService;
        public new StatusCode StatusCode { get; set; } = new();

        [HttpGet("get-dashboard-admin")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetDashBoardAdmin()
        {
            try
            {
                var response = await _dashboardService.GetDashBoardAdmin();
                return Ok(response);
            }
            catch (FormatException)
            {
                return Unauthorized(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Bạn chưa đăng nhập"),
                    Status = StatusCode.Unauthorized
                });
            }
            catch (Exception)
            {
                return BadRequest(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Lỗi hệ thống"),
                    Status = StatusCode.BadRequest
                });
            }
        }

        [HttpGet("get-dashboard-partner")]
        [Authorize(Roles = "PARTNER")]
        public async Task<IActionResult> GetDashBoardPartner()
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());
                var response = await _dashboardService.GetDashBoardPartner(userId);
                return Ok(response);
            }
            catch (FormatException)
            {
                return Unauthorized(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Bạn chưa đăng nhập"),
                    Status = StatusCode.Unauthorized
                });
            }
            catch (Exception)
            {
                return BadRequest(new ResponseObject<string>()
                {
                    Payload = new Payload<string>(string.Empty, "Lỗi hệ thống"),
                    Status = StatusCode.BadRequest
                });
            }
        }
    }
}
