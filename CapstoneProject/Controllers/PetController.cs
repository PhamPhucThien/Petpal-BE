﻿using CapstoneProject.Business.Interface;
using CapstoneProject.DTO.Request.Pet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }
        
        
        
        [HttpPost("GetList")]
        public async Task<IActionResult> GetList(PetListRequest request)
        {
            try
            {
                var response = await _petService.GetList(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpGet("{petId}")]
        public async Task<IActionResult> GetPetById(string petId)
        {
            try
            {
                var response = await _petService.GetPetById(petId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpPost("Create")]
        public async Task<IActionResult> CreatePet(PetCreateRequest request)
        {
            try
            {
                var response = await _petService.CreatePet(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpPut("Update")]
        public async Task<IActionResult> UpdatePet(PetUpdateRequest request)
        {
            try
            {
                var response = await _petService.UpdatePet(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
