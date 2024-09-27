using CapstoneProject.Business.Interfaces;
using CapstoneProject.DTO.Request.Package;
using CapstoneProject.DTO.Request.Base;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CapstoneProject.Business;
using CapstoneProject.DTO;
using CapstoneProject.Infrastructure.Extension;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.CodeAnalysis;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO.Response.Account;
using CapstoneProject.Database.Model;
using CapstoneProject.Repository.Interface;
using CapstoneProject.DTO.Response.Package;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController(IPackageService packageService,IPackageRepository packageRepository) : ControllerBase
    {
        private readonly IPackageService _packageService = packageService;
        private readonly IPackageRepository _packageRepository = packageRepository;
        public new StatusCode StatusCode { get; set; } = new();

        [HttpPost("get-list-by-carecenter-id")]
        public async Task<IActionResult> GetListByCareCenterId(GetListPackageByCareCenterIdRequest request)
        {
            try
            {
                var response = await _packageService.GetListByCareCenterId(request);
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

        [HttpPost("get-by-id")]
        public async Task<IActionResult> GetById(GetPackageByIdRequest request)
        {
            try
            {
                var response = await _packageService.GetById(request);
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
        
        [HttpPost("get-list")]
        public async Task<IActionResult> GetList(ListRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());
                var response = await _packageService.GetList(request, userId);
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

        /*[HttpGet("get-package{packageId}")]
        public async Task<IActionResult> GetPackageById(string packageId)
        {
            try
            {
                var response = await _packageService.GetById(packageId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }*/

        [HttpPost("create")]
        [Authorize(Roles = "MANAGER")]
        public async Task<IActionResult> Create(
            [FromBody] PackageCreateRequest request
            )
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());

                /*FileDetails filesDetail = new();

                if (file != null && file.Length != 0)
                {
                    using var stream = new MemoryStream();
                    await file.CopyToAsync(stream);
                    filesDetail.FileName = Path.GetFileName(file.FileName);
                    filesDetail.TempPath = Path.GetTempFileName();
                    filesDetail.FileData = stream.ToArray();
                }
                else
                {
                    filesDetail.IsContain = false;
                }*/

                var response = await _packageService.Create(request, userId);
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

        [HttpPost("upload-package-image")]
        public async Task<IActionResult> UploadPackageImage([FromForm] Guid packageId, IFormFile file)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());

                FileDetails filesDetail = new();

                if (file != null && file.Length != 0)
                {
                    using var stream = new MemoryStream();
                    await file.CopyToAsync(stream);
                    filesDetail.FileName = Path.GetFileName(file.FileName);
                    filesDetail.TempPath = Path.GetTempFileName();
                    filesDetail.FileData = stream.ToArray();
                }
                else
                {
                    filesDetail.IsContain = false;
                }

                var response = await _packageService.UploadPackageImage(packageId, userId, filesDetail);
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
        
        [HttpPut("update-package")]
        public async Task<IActionResult> UpdatePackage(PackageUpdateRequest request)
        {
            try
            {
                var response = await _packageService.Update(request);
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
        [HttpDelete("delete-package")]
        public async Task<IActionResult> DeletePackage(Guid packageId)
        {
            ResponseObject<DeletePackageResponse> response = new();
            DeletePackageResponse data = new();

            Package? package = await _packageRepository.GetByIdAsync(packageId);

            if (package != null)
            {
                package.Status = BaseStatus.DISABLE;    

                await _packageRepository.EditAsync(package);

                data.IsSucceed = true;

                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Xóa gói thành công";
                response.Payload.Data = data;
            }
            else
            {
                data.IsSucceed = false;

                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Không thể tìm thấy gói";
                response.Payload.Data = data;
            }

            return Ok(response);
        }
    }
}
