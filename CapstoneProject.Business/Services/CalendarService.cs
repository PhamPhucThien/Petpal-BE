using AutoMapper;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Calendar;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Calendar;
using CapstoneProject.Repository.Interface;

namespace CapstoneProject.Business.Services
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
                Search = request.Search ?? string.Empty,
                MaxPage = 1
            };
            Tuple<List<Calendar>, int> listCalendar = await _calendarRepository.GetWithPaging(paging);
            List<CalendarResponse> listCalendarResponse = _mapper.Map<List<CalendarResponse>>(listCalendar.Item1);
            paging.MaxPage = listCalendar.Item2;
            BaseListResponse<CalendarResponse> response = new()
            {
                List = listCalendarResponse,
                Paging = paging,
            };
            return response;
        }

        public async Task<CalendarResponse> GetById(string calendarId)
        {
            Calendar? calendar = await _calendarRepository.GetByIdAsync(Guid.Parse(calendarId));
            if (calendar == null)
            {
                throw new Exception("Not found Calendar with this id");

            }
            CalendarResponse calendarResponse = _mapper.Map<CalendarResponse>(calendar);
            return calendarResponse;
        }

        public async Task<CalendarResponse> Create(CalendarCreateRequest request)
        {
            CareCenter? careCenter = await _careCenterRepository.GetByIdAsync(Guid.Parse(request.CareCenterId));
            if (careCenter == null)
            {
                throw new Exception("Carecenter id is invalid.");
            }

            Calendar calendarCreate = _mapper.Map<Calendar>(request);
            calendarCreate.CreatedAt = DateTimeOffset.Now;
            Calendar? result = await _calendarRepository.AddAsync(calendarCreate);
            Calendar? blog = await _calendarRepository.GetByIdAsync(result.Id);
            return _mapper.Map<CalendarResponse>(blog);
        }

        public async Task<CalendarResponse?> Update(CalendarUpdateRequest request)
        {
            Calendar? calendarCheck = await _calendarRepository.GetByIdAsync(Guid.Parse(request.Id));
            if (calendarCheck == null)
            {
                throw new Exception("ID is invalid.");
            }

            CareCenter? careCenter = await _careCenterRepository.GetByIdAsync(Guid.Parse(request.CareCenterId));
            if (careCenter == null)
            {
                throw new Exception("Carecenter id is invalid.");
            }

            Calendar calendarUpdate = _mapper.Map<Calendar>(request);
            calendarUpdate.CreatedAt = calendarCheck.CreatedAt;
            calendarUpdate.CreatedBy = calendarCheck.CreatedBy;
            calendarUpdate.UpdatedAt = DateTimeOffset.Now;
            bool result = await _calendarRepository.EditAsync(calendarUpdate);
            return result ? _mapper.Map<CalendarResponse>(calendarUpdate) : null;
        }
    }
}
