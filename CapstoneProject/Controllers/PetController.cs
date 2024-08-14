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

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;

        public new StatusCode StatusCode { get; set; } = new();

        public PetController(IPetService petService)
        {
            _petService = petService;
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
                    Payload = new Payload<string>("", "Bạn chưa đăng nhập"),
                    Status = StatusCode.Unauthorized
                });
            }
            catch (Exception)
            {
                return BadRequest(new ResponseObject<string>()
                {
                    Payload = new Payload<string>("", "Lỗi hệ thống"),
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
                    Payload = new Payload<string>("", "Bạn chưa đăng nhập"),
                    Status = StatusCode.Unauthorized
                });
            }
            catch (Exception)
            {
                return BadRequest(new ResponseObject<string>()
                {
                    Payload = new Payload<string>("", "Lỗi hệ thống"),
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
                    Payload = new Payload<string>("", "Bạn chưa đăng nhập"),
                    Status = StatusCode.Unauthorized
                });
            }
            catch (Exception)
            {
                return BadRequest(new ResponseObject<string>()
                {
                    Payload = new Payload<string>("", "Lỗi hệ thống"),
                    Status = StatusCode.BadRequest
                });
            }
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
