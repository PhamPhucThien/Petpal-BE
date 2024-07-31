using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO.Response.Base;

namespace CapstoneProject.DTO.Response.User;

public class UserDetailResponse : BaseResponse
{
    public string? Username { get; set; }
    public string? FullName { get; set; }
    public string? Address { get; set; }
    public string? RoomId { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfileImage { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
}