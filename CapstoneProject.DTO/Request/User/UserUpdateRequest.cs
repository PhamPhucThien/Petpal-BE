using System.ComponentModel.DataAnnotations;
using CapstoneProject.Database.Model.Meta;

namespace CapstoneProject.DTO.Request.User;

public class UserUpdateRequest
{
    [Required]
    public string Id { get; set; }
    [Required]
    public string Fullname { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public string RoomId { get; set; }
    [Required]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number. Please enter a 10-digit number.")]
    public string PhoneNumber { get; set; }
    [Required]
    public string ProfileImage { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Status { get; set; }
}