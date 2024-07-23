using CapstoneProject.Business.Interface;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Calendar;
using CapstoneProject.DTO.Request.Comment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }
        
        [HttpPost("get-list")]
        public async Task<IActionResult> GetList(ListRequest request)
        
        {
            try
            {
                var response = await _calendarService.GetList(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpGet("get-calendar{calendarId}")]
        public async Task<IActionResult> GetCalendarById(string calendarId)
        {
            try
            {
                var response = await _calendarService.GetById(calendarId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpPost("create-calendar")]
        public async Task<IActionResult> CreateCalendar(CalendarCreateRequest request)
        {
            try
            {
                var response = await _calendarService.Create(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpPut("update-calendar")]
        public async Task<IActionResult> UpdateCalendar(CalendarUpdateRequest request)
        {
            try
            {
                var response = await _calendarService.Update(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
