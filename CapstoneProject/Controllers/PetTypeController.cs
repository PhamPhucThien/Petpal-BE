using CapstoneProject.Business.Interfaces;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Pet;
using CapstoneProject.DTO.Request.PetType;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.PetType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetTypeController : ControllerBase
    {
        private readonly IPetTypeService _petTypeService;

        public PetTypeController(IPetTypeService petTypeService)
        {
            _petTypeService = petTypeService;
        }
        
        [HttpPost("get-list")]
        public async Task<IActionResult> GetList(ListRequest request)
        {
            try
            {
                var listPetType = await _petTypeService.GetList(request);
                var response = new ResponseObject<BaseListResponse<PetTypeDetailResponse>>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Get all pet type successfully";
                response.Payload.Data = listPetType;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpGet("get-pet-type{petTypeId}")]
        public async Task<IActionResult> GetPetTypeById(string petTypeId)
        {
            try
            {
                var petTypeDetailResponse = await _petTypeService.GetById(petTypeId);
                var response = new ResponseObject<PetTypeDetailResponse>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Get pet type successfully";
                response.Payload.Data = petTypeDetailResponse;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpPost("create-pet-type")]
        public async Task<IActionResult> CreatePetType(PetTypeCreateRequest request)
        {
            try
            {
                var petTypeDetailResponse = await _petTypeService.Create(request);
                var response = new ResponseObject<PetTypeDetailResponse>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Create pet type successfully";
                response.Payload.Data = petTypeDetailResponse;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        [HttpPut("update-pet-type")]
        public async Task<IActionResult> UpdatePetType(PetTypeUpdateRequest request)
        {
            try
            {
                var petTypeDetailResponse = await _petTypeService.Update(request);
                var response = new ResponseObject<PetTypeDetailResponse>();
                response.Status = StatusCodes.Status200OK.ToString();
                response.Payload.Message = "Update pet type successfully";
                response.Payload.Data = petTypeDetailResponse;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
