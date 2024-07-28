using CapstoneProject.Business.Interface;
using CapstoneProject.Business.Service;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Blog;
using CapstoneProject.DTO.Request.Pet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController(IBlogService blogService) : ControllerBase
    {
        private readonly IBlogService _blogService = blogService;

        [HttpPost("get-list")]
        public async Task<IActionResult> GetList(ListRequest request)
        {
            try
            {
                var response = await _blogService.GetList(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpGet("get-blog{blogId}")]
        public async Task<IActionResult> GetPetById(string blogId)
        {
            try
            {
                var response = await _blogService.GetById(blogId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpPost("create-blog")]
        public async Task<IActionResult> CreatePet(BlogCreateRequest request)
        {
            try
            {
                var response = await _blogService.Create(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpPut("update-blog")]
        public async Task<IActionResult> UpdatePet(BlogUpdateRequest request)
        {
            try
            {
                var response = await _blogService.Update(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
