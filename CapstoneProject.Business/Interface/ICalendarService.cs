using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Calendar;
using CapstoneProject.DTO.Request.Comment;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Calendar;
using CapstoneProject.DTO.Response.Comment;

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
