using CapstoneProject.Business.Interface;
using CapstoneProject.Business.Service;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO.Request.CareCenters;
using CapstoneProject.DTO.Request.User;
using CapstoneProject.Infrastructure.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> CreateCareCenterAndManager(CreateCareCenterRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());

                var response = await _careCenterService.CreateCareCenterAndManager(userId, request);
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
        public async Task<IActionResult> RejestCareCenterRegistration([FromQuery] EditCareCenterRegistrationRequest request)
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