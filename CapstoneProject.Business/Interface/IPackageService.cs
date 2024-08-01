using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Package;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Package;

namespace CapstoneProject.Business.Interface
{
    public interface IPackageService
    {
        Task<ResponseObject<PackageResponseModel>> GetById(GetPackageByIdRequest request);
        Task<ResponseObject<ListPackageResponse>> GetListByCareCenterId(GetListPackageByCareCenterIdRequest request);
        Task<BaseListResponse<PackageResponse>> GetList(ListRequest request);
        Task<PackageResponse> Create(PackageCreareRequest request);
        Task<PackageResponse> Update(PackageUpdateRequest request);
    }
}
