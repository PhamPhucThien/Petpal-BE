using CapstoneProject.Business;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.Business.Services;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.User;
using CapstoneProject.DTO.Response;
using CapstoneProject.Infrastructure.Extension;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.CodeAnalysis;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public StatusCode StatusCode { get; set; } = new();

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /*[HttpPost("UploadImage")]
        public async Task<IActionResult> UploadProfile(FileStream file)
        {
            try
            {
                var response = await _userService.UploadProfile(file);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }*/
        
        [HttpPost("get-list")]
        public async Task<IActionResult> GetList(ListRequest request)
        {
            try
            {
                var userList = await _userService.GetList(request);
                var response = new ResponseObject<BaseListResponse<UserDetailResponse>>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Get all user successfully";
                response.Payload.Data = userList;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpGet("get-user-by-id")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            try
            {
                var response = await _userService.GetUserById(userId);
          
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("get-info")]
        public async Task<IActionResult> GetInfo()
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());

                var response = await _userService.GetUserById(userId);
              
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /*[HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(UserCreateRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());

                var response = await _userService.CreateUser(userId, request);
        
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }*/

        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(
            [FromForm] UserUpdateRequest request, 
            IFormFile file)
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

                var response = await _userService.UpdateUser(userId, request, filesDetail);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("count-user")]
        public async Task<IActionResult> CountUser()
        {
            try
            {
                var response = await _userService.CountUser();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("approve-partner-registration")]
        public async Task<IActionResult> ApprovePartnerRegistration(EditPartnerRegistrationRequest request)
        {
            try
            {
                var response = await _userService.ApprovePartnerRegistration(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("reject-partner-registration")]
        public async Task<IActionResult> RejestPartnerRegistration(EditPartnerRegistrationRequest request)
        {
            try
            {
                var response = await _userService.RejestPartnerRegistration(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("get-pending-partner")]
        public async Task<IActionResult> GetPendingPartner(ListRequest request)
        {
            try
            {
                var response = await _userService.GetUser(request, UserStatus.PENDING, UserRole.PARTNER);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("get-user")]
        public async Task<IActionResult> GetUser(ListUserRequest request)
        {
            try
            {
                var response = await _userService.GetUser(request.ListRequest, request.Status, request.Role);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
