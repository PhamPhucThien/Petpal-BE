using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneProject.DTO.Request.Pet;
using CapstoneProject.DTO.Response.Pet;

namespace CapstoneProject.Business.Interface
{
    public interface IPetService
    {
        Task<PetListResponse> GetList(PetListRequest request);
        Task<PetResponse> GetPetById(string petId);
        Task<PetResponse> CreatePet(PetCreateRequest request);
        Task<PetResponse> UpdatePet(PetUpdateRequest request);
    }
}
