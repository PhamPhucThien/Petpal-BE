using CapstoneProject.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Calendar;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Blog;
using CapstoneProject.DTO.Response.Calendar;
using CapstoneProject.Repository.Interface;

namespace CapstoneProject.Business.Service
{
    public class CalendarService : ICalendarService
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly ICareCenterRepository _careCenterRepository;
        private readonly IMapper _mapper;

        public CalendarService(ICalendarRepository calendarRepository, ICareCenterRepository careCenterRepository,
            IMapper mapper)
        {
            _calendarRepository = calendarRepository;
            _careCenterRepository = careCenterRepository;
            _mapper = mapper;
        }
        
        public async Task<BaseListResponse<CalendarResponse>> GetList(ListRequest request)
        {
            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                MaxPage = 1
            };
            var listCalendar = await _calendarRepository.GetWithPaging(paging);
            var listCalendarResponse = _mapper.Map<List<CalendarResponse>>(listCalendar);
            paging.Total = listCalendarResponse.Count;
            BaseListResponse<CalendarResponse> response = new()
            {
                List = listCalendarResponse,
                Paging = paging,
            };
            return response;
        }

        public async Task<CalendarResponse> GetById(string calendarId)
        {
            var calendar = await _calendarRepository.GetByIdAsync(Guid.Parse(calendarId));
            if (calendar == null)
            {
                throw new Exception("Not found Calendar with this id");
               
            }
            var calendarResponse = _mapper.Map<CalendarResponse>(calendar);
            return calendarResponse;
        }

        public async Task<CalendarResponse> Create(CalendarCreateRequest request)
        {
            var careCenter = await _careCenterRepository.GetByIdAsync(Guid.Parse(request.CareCenterId));
            if (careCenter == null)
            {
                throw new Exception("Carecenter id is invalid.");
            }
            
            var calendarCreate = _mapper.Map<Calendar>(request);
            calendarCreate.CreatedAt = DateTimeOffset.Now;
            var result = await _calendarRepository.AddAsync(calendarCreate);
            var blog = await _calendarRepository.GetByIdAsync(result.Id);
            return _mapper.Map<CalendarResponse>(blog);
        }

        public async Task<CalendarResponse> Update(CalendarUpdateRequest request)
        {
            var calendarCheck = await _calendarRepository.GetByIdAsync(Guid.Parse(request.Id));
            if (calendarCheck == null)
            {
                throw new Exception("ID is invalid.");
            }
            
            var careCenter =  await _careCenterRepository.GetByIdAsync(Guid.Parse(request.CareCenterId));
            if (careCenter == null)
            {
                throw new Exception("Carecenter id is invalid.");
            }

            var calendarUpdate = _mapper.Map<Calendar>(request);
            calendarUpdate.CreatedAt = calendarCheck.CreatedAt;
            calendarUpdate.CreatedBy = calendarCheck.CreatedBy;
            calendarUpdate.UpdatedAt = DateTimeOffset.Now;
            var result = await _calendarRepository.EditAsync(calendarUpdate);
            return result ? _mapper.Map<CalendarResponse>(calendarUpdate) : null;
        }
    }
}
