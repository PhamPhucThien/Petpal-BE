using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Pet;
using CapstoneProject.DTO.Request.PetType;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Pet;
using CapstoneProject.DTO.Response.PetType;

namespace CapstoneProject.Business.Interface
{
    public interface IPetTypeService
    {
        Task<BaseListResponse<PetTypeDetailResponse>> GetList(ListRequest request);
        Task<PetTypeDetailResponse> GetById(string petId);
        Task<PetTypeDetailResponse> Create(PetTypeCreateRequest request);
        Task<PetTypeDetailResponse> Update(PetTypeUpdateRequest request);
    }
}
