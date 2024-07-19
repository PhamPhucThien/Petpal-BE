using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.DTO.Request.PetType;

public class PetTypeUpdateRequest
{
    [Required(ErrorMessage = "Id is required")]
    public string Id { get; set; }
    [Required(ErrorMessage = "Type is required")]
    public string Type { get; set; }
    [Required(ErrorMessage = "Category is required")]
    public string Category { get; set; }
    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }
    [Required(ErrorMessage = "Status is required")]
    public string Status { get; set; }
    [Required(ErrorMessage = "UpdatedBy is required")]
    public string UpdatedBy { get; set; }
}