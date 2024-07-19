using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request.Blog;
using CapstoneProject.DTO.Response.Blog;
using CapstoneProject.DTO.Response.User;

namespace CapstoneProject.Infrastructure.Profile;

public class BlogProfile : AutoMapper.Profile
{
    public BlogProfile()
    {
        CreateMap<Blog, BlogResponse>();
        CreateMap<User, UserInforResponse>();
        CreateMap<BlogCreateRequest, Blog>();
        CreateMap<BlogUpdateRequest, Blog>();
    }
}