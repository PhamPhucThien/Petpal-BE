using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.DTO.Request.Calendar;

public class CalendarCreateRequest
{
    [Required(ErrorMessage = "CareCenterId is required")]
    public string CareCenterId { get; set; }
    [Required(ErrorMessage = "PetAmountList is required")]
    public string PetAmountList { get; set; }
    [Required(ErrorMessage = "Year is required")]
    public string Year { get; set; }
    [Required(ErrorMessage = "CreatedBy is required")]
    public string CreatedBy { get; set; }
}