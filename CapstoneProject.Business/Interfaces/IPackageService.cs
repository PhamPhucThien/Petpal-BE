using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Package;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Package;
using CapstoneProject.DTO.Response.Service;

namespace CapstoneProject.Business.Interfaces
{
    public interface IPackageService
    {
        Task<ResponseObject<PackageResponseModel>> GetById(GetPackageByIdRequest request);
        Task<ResponseObject<ListPackageResponse>> GetListByCareCenterId(GetListPackageByCareCenterIdRequest request);
        Task<ResponseObject<ListPackageResponse>> GetList(ListRequest request, Guid userId);
        Task<ResponseObject<CreatePackageResponse>> Create(PackageCreateRequest request, Guid userId);
        Task<PackageResponse> Update(PackageUpdateRequest request);
        Task<bool> DataGenerator();
        Task<ResponseObject<CreatePackageResponse>> UploadPackageImage(Guid packageId, Guid userId, FileDetails filesDetail);
    }
}
