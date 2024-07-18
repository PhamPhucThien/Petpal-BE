using System.ComponentModel.DataAnnotations;
using CapstoneProject.Database.Model.Meta;

namespace CapstoneProject.DTO.Request.User;

public class UserUpdateRequest
{
    [Required(ErrorMessage = "Id is required")]
    public string Id { get; set; }
    [Required(ErrorMessage = "Fullname is required")]
    public string Fullname { get; set; }
    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }
    [Required(ErrorMessage = "RoomId is required")]
    public string RoomId { get; set; }
    [Required(ErrorMessage = "PhoneNumber is required")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number. Please enter a 10-digit number.")]
    public string PhoneNumber { get; set; }
    [Required(ErrorMessage = "ProfileImage is required")]
    public string ProfileImage { get; set; }
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Status is required")]
    public string Status { get; set; }
    [Required(ErrorMessage = "UpdateBy is required")]
    public string UpdateBy { get; set; }
}