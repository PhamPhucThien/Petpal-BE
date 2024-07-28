using CapstoneProject.Business.Interface;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Package;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageItemController : ControllerBase
    {
        private readonly IPackageItemService _packageItemService;

        public PackageItemController(IPackageItemService packageItemService)
        {
            _packageItemService = packageItemService;
        }
        
        [HttpPost("get-list")]
        public async Task<IActionResult> GetList(ListRequest request)
        {
            try
            {
                var response = await _packageItemService.GetList(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpGet("get-package-item{packageItemId}")]
        public async Task<IActionResult> GetPackageItemById(string packageItemId)
        {
            try
            {
                var response = await _packageItemService.GetById(packageItemId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpPost("create-package-item")]
        public async Task<IActionResult> CreatePackage(PackageItemCreateRequest request)
        {
            try
            {
                var response = await _packageItemService.Create(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpPut("update-package-item")]
        public async Task<IActionResult> UpdatePackage(PackageItemUpdateRequest request)
        {
            try
            {
                var response = await _packageItemService.Update(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
