using CapstoneProject.Business.Interface;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.PetType;
using CapstoneProject.DTO.Request.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }
        
        [HttpPost("get-list")]
        public async Task<IActionResult> GetList(ListRequest request)
        {
            try
            {
                var response = await _serviceService.GetList(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpGet("get-service{serviceId}")]
        public async Task<IActionResult> GetServiceById(string serviceId)
        {
            try
            {
                var response = await _serviceService.GetById(serviceId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpPost("create-service")]
        public async Task<IActionResult> CreateService(ServiceCreateRequest request)
        {
            try
            {
                var response = await _serviceService.Create(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpPut("update-service")]
        public async Task<IActionResult> UpdateService(ServiceUpdateRequest request)
        {
            try
            {
                var response = await _serviceService.Update(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
