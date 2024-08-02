using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Blog;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Blog;

namespace CapstoneProject.Business.Interfaces
{
    public interface IBlogService
    {
        Task<BaseListResponse<BlogResponse>> GetList(ListRequest request);
        Task<BlogResponse> GetById(string blogId);
        Task<BlogResponse> Create(BlogCreateRequest request);
        Task<BlogResponse> Update(BlogUpdateRequest request);
    }
}
