using CapstoneProject.Business;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.PetType;
using CapstoneProject.DTO.Request.Service;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Service;
using CapstoneProject.Infrastructure.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        public new StatusCode StatusCode { get; set; } = new();
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }
        
        [HttpPost("get-list")]
        [Authorize(Roles = "ADMIN,PARTNER,MANAGER")]
        public async Task<IActionResult> GetList(ListRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());

                var response = await _serviceService.GetList(request, userId);

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
        
        [HttpGet("get-service/{serviceId}")]
        [Authorize(Roles = "PARTNER,MANAGER,ADMIN")]
        public async Task<IActionResult> GetServiceById(Guid serviceId)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());

                var response = await _serviceService.GetById(serviceId, userId);

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
        
        [HttpPost("create")]
        [Authorize(Roles = "PARTNER,ADMIN")]
        public async Task<IActionResult> Create(ServiceCreateRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());

                var response = await _serviceService.Create(request, userId);
               
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
        
        [HttpPut("update")]
        public async Task<IActionResult> UpdateService(ServiceUpdateRequest request)
        {
            try
            {
                var serviceResponse = await _serviceService.Update(request);
                var response = new ResponseObject<ServiceResponse>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Create service successfully";
                response.Payload.Data = serviceResponse;
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
