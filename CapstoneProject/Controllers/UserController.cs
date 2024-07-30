using CapstoneProject.Business.Interface;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.User;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

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
        
        [HttpGet("get-user{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            try
            {
                var userResponse = await _userService.GetUserById(userId);
                var response = new ResponseObject<UserDetailResponse>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Get user successfully";
                response.Payload.Data = userResponse;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(UserCreateRequest request)
        {
            try
            {
                var userResponse = await _userService.CreateUser(request);
                var response = new ResponseObject<UserDetailResponse>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Create user successfully";
                response.Payload.Data = userResponse;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(UserUpdateRequest request)
        {
            try
            {
                var userResponse = await _userService.UpdateUser(request);
                var response = new ResponseObject<UserDetailResponse>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Update user successfully";
                response.Payload.Data = userResponse;
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

        /*[HttpGet("get-pending-partner")]
        public async Task<IActionResult> GetPendingPartner(Paging paging)
        {
            try
            {
                var response = await _userService.GetPendingParter(paging, UserStatus.PENDING);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("get-active-partner")]
        public async Task<IActionResult> GetPendingPartner(Paging paging)
        {
            try
            {
                var response = await _userService.GetPendingParter(paging, UserStatus.PENDING);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }*/
    }
}
