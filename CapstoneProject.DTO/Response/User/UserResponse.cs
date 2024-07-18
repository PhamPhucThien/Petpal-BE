using CapstoneProject.Database.Model.Meta;

namespace CapstoneProject.DTO.Response.User;

public class UserResponse
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public string RoomId { get; set; }
    public string PhoneNumber { get; set; }
    public string ProfileImage { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }
    public string CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
    public string Role { get; set; }
}