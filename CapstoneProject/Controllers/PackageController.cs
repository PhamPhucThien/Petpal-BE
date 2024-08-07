﻿using CapstoneProject.Business.Interfaces;
using CapstoneProject.DTO.Request.Package;
using CapstoneProject.DTO.Request.Base;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController(IPackageService packageService) : ControllerBase
    {
        private readonly IPackageService _packageService = packageService;

        [HttpPost("get-list-by-carecenter-id")]
        public async Task<IActionResult> GetListByCareCenterId(GetListPackageByCareCenterIdRequest request)
        {
            try
            {
                var response = await _packageService.GetListByCareCenterId(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("get-by-id")]
        public async Task<IActionResult> GetById(GetPackageByIdRequest request)
        {
            try
            {
                var response = await _packageService.GetById(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpPost("get-list")]
        public async Task<IActionResult> GetList(ListRequest request)
        {
            try
            {
                var response = await _packageService.GetList(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        /*[HttpGet("get-package{packageId}")]
        public async Task<IActionResult> GetPackageById(string packageId)
        {
            try
            {
                var response = await _packageService.GetById(packageId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }*/
        
        [HttpPost("create-package")]
        public async Task<IActionResult> CreatePackage(PackageCreareRequest request)
        {
            try
            {
                var response = await _packageService.Create(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpPut("update-package")]
        public async Task<IActionResult> UpdatePackage(PackageUpdateRequest request)
        {
            try
            {
                var response = await _packageService.Update(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
