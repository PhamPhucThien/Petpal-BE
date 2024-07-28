using CapstoneProject.Business.Interface;
using CapstoneProject.Business.Service;
using CapstoneProject.DTO.Request.Package;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController(IPackageService packageService) : ControllerBase
    {
        private readonly IPackageService _packageService = packageService;

        [HttpPost("get-list-by-carecenter-id")]
        public async Task<IActionResult> GetListByCareCenterId(GetListPackageByCareCenterIdRequest request)
        {
            try
            {
                var response = await _packageService.GetListByCareCenterId(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("get-by-id")]
        public async Task<IActionResult> GetById(GetPackageByIdRequest request)
        {
            try
            {
                var response = await _packageService.GetById(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
