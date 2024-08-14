using CapstoneProject.Business;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.DTO;
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

        public new StatusCode StatusCode { get; set; } = new();
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
        
        [HttpGet("get-package-item/{packageItemId}")]
        public async Task<IActionResult> GetPackageItemById(string packageItemId)
        {
            try
            {
                var response = await _packageItemService.GetById(packageItemId);
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
        
        [HttpPost("create-package-item")]
        public async Task<IActionResult> CreatePackage(PackageItemCreateRequest request)
        {
            try
            {
                var response = await _packageItemService.Create(request);
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
        
        [HttpPut("update-package-item")]
        public async Task<IActionResult> UpdatePackage(PackageItemUpdateRequest request)
        {
            try
            {
                var response = await _packageItemService.Update(request);
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
