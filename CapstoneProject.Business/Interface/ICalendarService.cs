using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Calendar;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Calendar;

namespace CapstoneProject.Business.Interface
{
    public interface ICalendarService
    {
        Task<BaseListResponse<CalendarResponse>> GetList(ListRequest request);
        Task<CalendarResponse> GetById(string calendarId);
        Task<CalendarResponse> Create(CalendarCreateRequest request);
        Task<CalendarResponse> Update(CalendarUpdateRequest request);
    }
}
