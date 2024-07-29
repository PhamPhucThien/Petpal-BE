using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Pet;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Pet;

namespace CapstoneProject.Business.Interface
{
    public interface IPetService
    {
        Task<ResponseObject<ListPetModel>> GetList(Guid userId, ListRequest request);
        Task<PetResponse> GetPetById(string petId);
        Task<ResponseObject<CreatePetResponse>> CreatePet(Guid userId, PetCreateRequest request);
        Task<PetResponse> UpdatePet(PetUpdateRequest request);
    }
}
