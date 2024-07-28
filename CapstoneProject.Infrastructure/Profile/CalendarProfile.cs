using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request.Calendar;
using CapstoneProject.DTO.Response.Calendar;

namespace CapstoneProject.Infrastructure.Profile;

public class CalendarProfile : AutoMapper.Profile
{
    public CalendarProfile()
    {
        CreateMap<Calendar, CalendarResponse>();
        CreateMap<CalendarCreateRequest, Calendar>();
        CreateMap<CalendarUpdateRequest, Calendar>();
    }
}