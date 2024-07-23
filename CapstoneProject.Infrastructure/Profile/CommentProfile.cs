using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request.Blog;
using CapstoneProject.DTO.Request.Comment;
using CapstoneProject.DTO.Response.Comment;
using CapstoneProject.DTO.Response.User;

namespace CapstoneProject.Infrastructure.Profile;

public class CommentProfile : AutoMapper.Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentResponse>();
        CreateMap<User, UserInforResponse>();
        CreateMap<CommentCreateRequest, Comment>();
        CreateMap<CommentUpdateRequest, Comment>();
    }
}