using CapstoneProject.Business;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.Business.Services;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Blog;
using CapstoneProject.DTO.Request.Pet;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Blog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController(IBlogService blogService) : ControllerBase
    {
        private readonly IBlogService _blogService = blogService;

        public new StatusCode StatusCode { get; set; } = new();

        [HttpPost("get-list")]
        public async Task<IActionResult> GetList(ListRequest request)
        {
            try
            {
                var listResponse = await _blogService.GetList(request);
                var response = new ResponseObject<BaseListResponse<BlogResponse>>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Get all successfully";
                response.Payload.Data = listResponse;
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
        
        [HttpGet("get-blog/{blogId}")]
        public async Task<IActionResult> getBlogById(string blogId)
        {
            try
            {
                var blogResponse = await _blogService.GetById(blogId);
                var response = new ResponseObject<BlogResponse>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Get blog successfully";
                response.Payload.Data = blogResponse;
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
        
        [HttpPost("create-blog")]
        public async Task<IActionResult> CreatePet(BlogCreateRequest request)
        {
            try
            {
                var blogResponse = await _blogService.Create(request);
                var response = new ResponseObject<BlogResponse>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Create successfully";
                response.Payload.Data = blogResponse;
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
        
        [HttpPut("update-blog")]
        public async Task<IActionResult> UpdatePet(BlogUpdateRequest request)
        {
            try
            {
                var blogResponse = await _blogService.Update(request);
                var response = new ResponseObject<BlogResponse>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Update successfully";
                response.Payload.Data = blogResponse;
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
    }
}
