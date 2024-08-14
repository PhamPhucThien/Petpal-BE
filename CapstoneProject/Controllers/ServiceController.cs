﻿using CapstoneProject.Business;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.PetType;
using CapstoneProject.DTO.Request.Service;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        public new StatusCode StatusCode { get; set; } = new();
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }
        
        [HttpPost("get-list")]
        public async Task<IActionResult> GetList(ListRequest request)
        {
            try
            {
                var serviceList = await _serviceService.GetList(request);
                var response = new ResponseObject<BaseListResponse<ServiceResponse>>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Get all service successfully";
                response.Payload.Data = serviceList;
                return Ok(response);
            }
            catch (FormatException)
            {
                return Unauthorized(new ResponseObject<string>()
                {
                    Payload = new Payload<string>("", "Bạn chưa đăng nhập"),
                    Status = StatusCode.Unauthorized
                });
            }
            catch (Exception)
            {
                return BadRequest(new ResponseObject<string>()
                {
                    Payload = new Payload<string>("", "Lỗi hệ thống"),
                    Status = StatusCode.BadRequest
                });
            }
        }
        
        [HttpGet("get-service/{serviceId}")]
        public async Task<IActionResult> GetServiceById(string serviceId)
        {
            try
            {
                var serviceResponse = await _serviceService.GetById(serviceId);
                var response = new ResponseObject<ServiceResponse>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Get service successfully";
                response.Payload.Data = serviceResponse;
                return Ok(response);
            }
            catch (FormatException)
            {
                return Unauthorized(new ResponseObject<string>()
                {
                    Payload = new Payload<string>("", "Bạn chưa đăng nhập"),
                    Status = StatusCode.Unauthorized
                });
            }
            catch (Exception)
            {
                return BadRequest(new ResponseObject<string>()
                {
                    Payload = new Payload<string>("", "Lỗi hệ thống"),
                    Status = StatusCode.BadRequest
                });
            }
        }
        
        [HttpPost("create-service")]
        public async Task<IActionResult> CreateService(ServiceCreateRequest request)
        {
            try
            {
                var serviceResponse = await _serviceService.Create(request);
                var response = new ResponseObject<ServiceResponse>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Create service successfully";
                response.Payload.Data = serviceResponse;
                return Ok(response);
            }
            catch (FormatException)
            {
                return Unauthorized(new ResponseObject<string>()
                {
                    Payload = new Payload<string>("", "Bạn chưa đăng nhập"),
                    Status = StatusCode.Unauthorized
                });
            }
            catch (Exception)
            {
                return BadRequest(new ResponseObject<string>()
                {
                    Payload = new Payload<string>("", "Lỗi hệ thống"),
                    Status = StatusCode.BadRequest
                });
            }
        }
        
        [HttpPut("update-service")]
        public async Task<IActionResult> UpdateService(ServiceUpdateRequest request)
        {
            try
            {
                var serviceResponse = await _serviceService.Update(request);
                var response = new ResponseObject<ServiceResponse>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Create service successfully";
                response.Payload.Data = serviceResponse;
                return Ok(response);
            }
            catch (FormatException)
            {
                return Unauthorized(new ResponseObject<string>()
                {
                    Payload = new Payload<string>("", "Bạn chưa đăng nhập"),
                    Status = StatusCode.Unauthorized
                });
            }
            catch (Exception)
            {
                return BadRequest(new ResponseObject<string>()
                {
                    Payload = new Payload<string>("", "Lỗi hệ thống"),
                    Status = StatusCode.BadRequest
                });
            }
        }
    }
}
