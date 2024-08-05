using CapstoneProject.Business.Interfaces;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Blog;
using CapstoneProject.DTO.Request.Comment;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Comment;
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
                var listComment = await _commentService.GetList(request);
                var response = new ResponseObject<BaseListResponse<CommentResponse>>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Get all comment successfully";
                response.Payload.Data = listComment;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpGet("get-comment/{commentId}")]
        public async Task<IActionResult> GetCommentById(string commentId)
        {
            try
            {
                var commentResponse = await _commentService.GetById(commentId);
                var response = new ResponseObject<CommentResponse>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Get comment successfully";
                response.Payload.Data = commentResponse;
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
                var commentResponse = await _commentService.Create(request);
                var response = new ResponseObject<CommentResponse>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Create comment successfully";
                response.Payload.Data = commentResponse;
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
                var commentResponse = await _commentService.Update(request);
                var response = new ResponseObject<CommentResponse>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Update comment successfully";
                response.Payload.Data = commentResponse;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
