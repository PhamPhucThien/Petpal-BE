

using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.DTO.Request.User;

public class UserCreateRequest
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string FullName { get; set; }
    [Required]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number. Please enter a 10-digit number.")]
    public string PhoneNumber { get; set; }
}