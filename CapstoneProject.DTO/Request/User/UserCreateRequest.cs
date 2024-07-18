

using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.DTO.Request.User;

public class UserCreateRequest
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    [Required(ErrorMessage = "FullName is required")]
    public string FullName { get; set; }
    [Required(ErrorMessage = "PhoneNumber is required")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number. Please enter a 10-digit number.")]
    public string PhoneNumber { get; set; }
    [Required(ErrorMessage = "CreateBy is required")]
    public string CreateBy { get; set; }
}