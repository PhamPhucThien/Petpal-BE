using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.PetType;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.PetType;

namespace CapstoneProject.Business.Interfaces
{
    public interface IPetTypeService
    {
        Task<ResponseObject<List<PetTypeDetailResponse>>> GetList(ListRequest request);
        Task<ResponseObject<PetTypeDetailResponse>> GetById(string petId);
        Task<ResponseObject<PetTypeDetailResponse>> Create(PetTypeCreateRequest request);
        Task<ResponseObject<PetTypeDetailResponse>> Update(PetTypeUpdateRequest request);
    }
}
