using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.DTO.Request.Calendar;

public class CalendarUpdateRequest
{
    [Required(ErrorMessage = "Id is required")]
    public string Id { get; set; }
    [Required(ErrorMessage = "CareCenterId is required")]
    public string CareCenterId { get; set; }
    [Required(ErrorMessage = "PetAmountList is required")]
    public string PetAmountList { get; set; }
    [Required(ErrorMessage = "Year is required")]
    public string Year { get; set; }
    [Required(ErrorMessage = "Status is required")]
    public string Status { get; set; }
    [Required(ErrorMessage = "CreatedBy is required")]
    public string UpdatedBy { get; set; }
}