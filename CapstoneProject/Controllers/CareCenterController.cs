using CapstoneProject.Business.Interface;
using CapstoneProject.Business.Service;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.CareCenters;
using CapstoneProject.DTO.Request.User;
using CapstoneProject.Infrastructure.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareCenterController(ICareCenterService careCenterService) : ControllerBase
    {
        private readonly ICareCenterService _careCenterService = careCenterService;

        [HttpPost("get-list")]
        public async Task<IActionResult> GetList(GetCareCenterListRequest request)
        {
            try
            {
                var response = await _careCenterService.GetList(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("create-carecenter-and-manager")]
        [Authorize(Roles = "PARTNER")]
        public async Task<IActionResult> CreateCareCenterAndManager(CreateCareCenterRequest request, IFormFile front_identity, IFormFile back_identity)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());

                FileDetails front_image = new();
                FileDetails back_image = new();

                using var stream = new MemoryStream();

                await front_identity.CopyToAsync(stream);
                front_image.FileName = Path.GetFileName(front_identity.FileName);
                front_image.TempPath = Path.GetTempFileName();
                front_image.FileData = stream.ToArray();

                back_image.FileName = Path.GetFileName(back_identity.FileName);
                back_image.TempPath = Path.GetTempFileName();
                back_image.FileData = stream.ToArray();

                var response = await _careCenterService.CreateCareCenterAndManager(userId, request, front_image, back_image);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("approve-carecenter-registration")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ApproveCareCenterRegistration([FromQuery] EditCareCenterRegistrationRequest request)
        {
            try
            {
                var response = await _careCenterService.EditCareCenterRegistration(request, CareCenterStatus.ACTIVE, UserStatus.ACTIVE);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("reject-carecenter-registration")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> RejectCareCenterRegistration([FromQuery] EditCareCenterRegistrationRequest request)
        {
            try
            {
                var response = await _careCenterService.EditCareCenterRegistration(request, CareCenterStatus.REJECTED, UserStatus.REJECTED);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}