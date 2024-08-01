﻿using CapstoneProject.Business;
using CapstoneProject.Business.Interface;
using CapstoneProject.Business.Service;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.User;
using CapstoneProject.DTO.Response;
using CapstoneProject.Infrastructure.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                var response = await _userService.GetList(request);
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
                var response = await _userService.GetUserById(userId);
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
                var response = await _userService.CreateUser(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(List<IFormFile> files, UserUpdateRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());
                List<FileDetails> filesDetail = [];

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        FileDetails data = new();
                        using var stream = new MemoryStream();
                        await file.CopyToAsync(stream);
                        data.FileName = Path.GetFileName(file.FileName);
                        data.TempPath = Path.GetTempFileName();
                        data.FileData = stream.ToArray();
                        filesDetail.Add(data);
                    }
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
        public async Task<IActionResult> GetUser(ListRequest request)
        {
            try
            {
                var response = await _userService.GetUser(request, null, null);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
