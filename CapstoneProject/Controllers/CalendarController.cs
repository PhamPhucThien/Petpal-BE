﻿using CapstoneProject.Business.Interfaces;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Calendar;
using CapstoneProject.DTO.Request.Comment;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Blog;
using CapstoneProject.DTO.Response.Calendar;
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
                var calendarResponse = await _calendarService.GetList(request);
                var response = new ResponseObject<BaseListResponse<CalendarResponse>>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Get all calendar successfully";
                response.Payload.Data = calendarResponse;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpGet("get-calendar/{calendarId}")]
        public async Task<IActionResult> GetCalendarById(string calendarId)
        {
            try
            {
                var calendarResponse = await _calendarService.GetById(calendarId);
                var response = new ResponseObject<CalendarResponse>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Get all calendar successfully";
                response.Payload.Data = calendarResponse;
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
                var calendarResponse = await _calendarService.Create(request);
                var response = new ResponseObject<CalendarResponse>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Create calendar successfully";
                response.Payload.Data = calendarResponse;
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
                var calendarResponse = await _calendarService.Update(request);
                var response = new ResponseObject<CalendarResponse>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Update calendar successfully";
                response.Payload.Data = calendarResponse;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
