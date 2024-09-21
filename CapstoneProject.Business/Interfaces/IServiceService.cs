using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Service;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Service;

namespace CapstoneProject.Business.Interfaces
{
    public interface IServiceService
    {
        Task<ResponseObject<ListServiceResponse>> GetList(ListRequest request, Guid userId);
        Task<ResponseObject<ServiceResponseModel>> GetById(Guid serviceId, Guid userId);
        Task<ResponseObject<CreateServiceResponse>> Create(ServiceCreateRequest request, Guid userId);
        Task<ServiceResponse> Update(ServiceUpdateRequest request);
    }
}
