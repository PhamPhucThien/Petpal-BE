using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Pet;
using CapstoneProject.DTO.Response.Pet;

namespace CapstoneProject.Business.Interfaces
{
    public interface IPetService
    {
        Task<ResponseObject<ListPetModel>> GetList(Guid userId, ListRequest request);
        Task<PetResponse> GetPetById(string petId);
        Task<ResponseObject<CreatePetResponse>> CreatePet(Guid userId, PetCreateRequest request, FileDetails fileDetails);
        Task<ResponseObject<GetCareCenterPetListResponse>> GetCareCenterPetList(Guid userId, ListRequest request);
        Task<ResponseObject<ListPetModel>> GetActiveByUserIdAndPetTypeId(Guid userId, Guid packageId, ListRequest request);
        Task<ResponseObject<CheckInAndOutResponse>> CheckIn(Guid userId, Guid petId, bool isCheckIn, FileDetails filesDetail);
        Task<ResponseObject<CheckInAndOutResponse>> CheckOut(Guid userId, Guid petId, bool isCheckOut, FileDetails filesDetail);
    }
}
