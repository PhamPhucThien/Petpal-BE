using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.PetType;
using CapstoneProject.DTO.Request.Service;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.PetType;
using CapstoneProject.DTO.Response.Service;

namespace CapstoneProject.Business.Interface
{
    public interface IServiceService
    {
        Task<BaseListResponse<ServiceResponse>> GetList(ListRequest request);
        Task<ServiceResponse> GetById(string serviceId);
        Task<ServiceResponse> Create(ServiceCreateRequest request);
        Task<ServiceResponse> Update(ServiceUpdateRequest request);
    }
}
