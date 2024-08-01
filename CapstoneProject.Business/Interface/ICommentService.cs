using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Comment;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Comment;

namespace CapstoneProject.Business.Interface
{
    public interface ICommentService
    {
        Task<BaseListResponse<CommentResponse>> GetList(ListRequest request);
        Task<CommentResponse> GetById(string commentId);
        Task<CommentResponse> Create(CommentCreateRequest request);
        Task<CommentResponse> Update(CommentUpdateRequest request);
    }
}
