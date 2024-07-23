using CapstoneProject.Business.Interface;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Blog;
using CapstoneProject.DTO.Request.Comment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        
        [HttpPost("get-list")]
        public async Task<IActionResult> GetList(ListRequest request)
        {
            try
            {
                var response = await _commentService.GetList(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpGet("get-comment{commentId}")]
        public async Task<IActionResult> GetCommentById(string commentId)
        {
            try
            {
                var response = await _commentService.GetById(commentId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpPost("create-comment")]
        public async Task<IActionResult> CreateComment(CommentCreateRequest request)
        {
            try
            {
                var response = await _commentService.Create(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpPut("update-comment")]
        public async Task<IActionResult> UpdateComment(CommentUpdateRequest request)
        {
            try
            {
                var response = await _commentService.Update(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
