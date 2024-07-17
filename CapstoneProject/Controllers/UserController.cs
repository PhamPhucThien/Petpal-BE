using CapstoneProject.Business.Interface;
using CapstoneProject.DTO.Request.User;
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
        
        [HttpPost("GetList")]
        public async Task<IActionResult> GetList(UserListRequest request)
        {
            try
            {
                var response = await _userService.GetList(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetUserById(string UserId)
        {
            try
            {
                var response = await _userService.GetUserById(UserId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser(UserCreateRequest request)
        {
            try
            {
                var response = await _userService.CreateUser(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUser(UserUpdateRequest request)
        {
            try
            {
                var response = await _userService.UpdateUser(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
