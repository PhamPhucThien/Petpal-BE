using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.User;

namespace CapstoneProject.DTO.Response.Comment;

public class CommentResponse : BaseResponse
{
    public string Content { get; set; }
    public int LikeNumber { get; set; }
    public UserInforResponse User { get; set; }
    public CommentResponse ParentComment { get; set; }
}