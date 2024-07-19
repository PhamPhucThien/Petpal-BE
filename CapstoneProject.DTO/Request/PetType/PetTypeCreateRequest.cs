using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.DTO.Request.PetType;

public class PetTypeCreateRequest
{
    [Required(ErrorMessage = "Type is required")]
    public string Type { get; set; }
    [Required(ErrorMessage = "Category is required")]
    public string Category { get; set; }
    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }
    [Required(ErrorMessage = "CreatedBy is required")]
    public string CreatedBy { get; set; }
}