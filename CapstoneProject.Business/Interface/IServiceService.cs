using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Service;
using CapstoneProject.DTO.Response.Base;
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
