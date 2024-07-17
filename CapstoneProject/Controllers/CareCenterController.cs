using CapstoneProject.Business.Interface;
using CapstoneProject.Business.Service;
using CapstoneProject.DTO.Request.CareCenters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareCenterController : ControllerBase
    {
        private readonly ICareCenterService _careCenterService;

        public CareCenterController(ICareCenterService careCenterService)
        {
            _careCenterService = careCenterService;
        }

        [HttpPost("GetList")]
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
    }
}
