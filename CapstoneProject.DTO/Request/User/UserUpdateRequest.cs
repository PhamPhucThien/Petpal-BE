using System.ComponentModel.DataAnnotations;
using CapstoneProject.Database.Model.Meta;

namespace CapstoneProject.DTO.Request.User;

public class UserUpdateRequest
{
    public string? Fullname { get; set; }
    public string? Address { get; set; }
    public string? RoomId { get; set; }
    [StringLength(10, MinimumLength = 10, ErrorMessage = "Độ dài số điện thoại phải là 10 số")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Chỉ được nhập số")]
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}