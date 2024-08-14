using CapstoneProject.Business;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.Business.Services;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.CareCenters;
using CapstoneProject.DTO.Request.User;
using CapstoneProject.Infrastructure.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareCenterController(ICareCenterService careCenterService) : ControllerBase
    {
        private readonly ICareCenterService _careCenterService = careCenterService;

        public new StatusCode StatusCode { get; set; } = new();

        [HttpPost("get-list")]
        public async Task<IActionResult> GetList(ListRequest request)
        {
            try
            {
                var response = await _careCenterService.GetList(request);
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

        [HttpPost("create-carecenter-and-manager")]
        [Authorize(Roles = "PARTNER")]
        public async Task<IActionResult> CreateCareCenterAndManager(
            [FromForm] CreateCareCenterRequest request,
            IFormFile front_identity,
            IFormFile back_identity,
            IFormFile carecenter)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());

                FileDetails front_image = new();
                FileDetails back_image = new();
                FileDetails carecenter_image = new();

                // Kiểm tra ảnh id phía trước
                if (front_identity != null && front_identity.Length != 0)
                {
                    using var stream = new MemoryStream();
                    await front_identity.CopyToAsync(stream);
                    front_image.FileName = Path.GetFileName(front_identity.FileName);
                    front_image.TempPath = Path.GetTempFileName();
                    front_image.FileData = stream.ToArray();
                }
                else
                {
                    front_image.IsContain = false;
                }

                // Kiểm tra ảnh id phía sau
                if (back_identity != null && back_identity.Length != 0)
                {
                    using var stream = new MemoryStream();
                    await back_identity.CopyToAsync(stream);
                    back_image.FileName = Path.GetFileName(back_identity.FileName);
                    back_image.TempPath = Path.GetTempFileName();
                    back_image.FileData = stream.ToArray();
                }
                else
                {
                    back_image.IsContain = false;
                }

                // Kiểm tra ảnh carecenter
                if (carecenter != null && carecenter.Length != 0)
                {
                    using var stream = new MemoryStream();
                    await carecenter.CopyToAsync(stream);
                    carecenter_image.FileName = Path.GetFileName(carecenter.FileName);
                    carecenter_image.TempPath = Path.GetTempFileName();
                    carecenter_image.FileData = stream.ToArray();
                }
                else
                {
                    carecenter_image.IsContain = false;
                }                

                var response = await _careCenterService.CreateCareCenterAndManager(userId, request, front_image, back_image, carecenter_image);
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

        [HttpPost("approve-carecenter-registration")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ApproveCareCenterRegistration(EditCareCenterRegistrationRequest request)
        {
            try
            {
                var response = await _careCenterService.EditCareCenterRegistration(request, CareCenterStatus.ACTIVE, UserStatus.ACTIVE);
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

        [HttpPost("reject-carecenter-registration")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> RejectCareCenterRegistration(EditCareCenterRegistrationRequest request)
        {
            try
            {
                var response = await _careCenterService.EditCareCenterRegistration(request, CareCenterStatus.REJECTED, UserStatus.REJECTED);
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

        [HttpPost("get-carecenter-by-role")]
        [Authorize(Roles = "PARTNER,MANAGER")]
        public async Task<IActionResult> GetCareCenterByRole(ListRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());
                var response = await _careCenterService.GetCareCenterByRole(userId, request);
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