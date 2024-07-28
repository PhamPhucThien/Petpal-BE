using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Response.Base;

namespace CapstoneProject.DTO.Response.Calendar;

public class CalendarResponse : BaseResponse
{
    public string CareCenterId { get; set; }
    public string PetAmountList { get; set; }
    public string Year { get; set; }
    public CareCenter CareCenter { get; set; }
}