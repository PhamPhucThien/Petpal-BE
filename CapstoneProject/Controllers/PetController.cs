using CapstoneProject.Business;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Pet;
using CapstoneProject.Infrastructure.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Response.Package;
using CapstoneProject.Repository.Interface;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;
        private readonly IPetRepository _petRepository;


        public new StatusCode StatusCode { get; set; } = new();

        public PetController(IPetService petService,IPetRepository petRepository)
        {
            _petService = petService;
            _petRepository = petRepository;
        }        
        
        [HttpPost("get-list")]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<IActionResult> GetList(ListRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());
                var response = await _petService.GetList(userId, request);
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

        [HttpPost("get-active-list")]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<IActionResult> GetActiveByUserIdAndPetTypeId(ListRequest request, [FromQuery] Guid packageId)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());
                var response = await _petService.GetActiveByUserIdAndPetTypeId(userId, packageId, request);
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

        [HttpPost("check-in")]
        [Authorize(Roles = "STAFF")]
        public async Task<IActionResult> CheckIn(Guid petId, bool isCheckIn, IFormFile? file)
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

                var response = await _petService.CheckIn(userId, petId, isCheckIn, filesDetail);
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

        [HttpPost("check-out")]
        [Authorize(Roles = "STAFF")]
        public async Task<IActionResult> CheckOut(Guid petId, bool isCheckOut, IFormFile? file)
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

                var response = await _petService.CheckOut(userId, petId, isCheckOut, filesDetail);
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

        [HttpPost("get-care-center-pet-list")]
        [Authorize(Roles = "CUSTOMER,STAFF")]
        public async Task<IActionResult> GetCareCenterPetList(ListRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());
                var response = await _petService.GetCareCenterPetList(userId, request);
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

        [HttpGet("get-pet/{petId}")]
        public async Task<IActionResult> GetPetById(string petId)
        {
            try
            {
                var response = await _petService.GetPetById(petId);
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
        
        [HttpPost("create-pet")]
        public async Task<IActionResult> CreatePet([FromForm] PetCreateRequest request, IFormFile file)
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

                var response = await _petService.CreatePet(userId, request, filesDetail);
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
        public async Task<IActionResult> DeletePackage(Guid petId)
        {
            ResponseObject<DeletePackageResponse> response = new();
            DeletePackageResponse data = new();

            Pet? pet = await _petRepository.GetByIdAsync(petId);

            if (pet != null)
            {
                pet.Status = PetStatus.DISABLE;

                await _petRepository.EditAsync(pet);

                data.IsSucceed = true;

                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Xóa thú cưng thành công";
                response.Payload.Data = data;
            }
            else
            {
                data.IsSucceed = false;

                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Không thể tìm thấy thú cưng";
                response.Payload.Data = data;
            }

            return Ok(response);
        }

        /* [HttpPut("update-pet")]
         public async Task<IActionResult> UpdatePet(PetUpdateRequest request)
         {
             try
             {
                 var response = await _petService.UpdatePet(request);
                 return Ok(response);
             }
             catch (Exception ex)
             {
                 return StatusCode(500, $"An error occurred: {ex.Message}");
             }
         }*/
    }
}
