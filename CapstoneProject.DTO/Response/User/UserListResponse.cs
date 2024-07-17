using CapstoneProject.DTO.Request;

using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Response.User;

public class UserListResponse
{
    public List<UserResponse>? List { get; set; }
    public Paging Paging { get; set; } = new Paging();
}