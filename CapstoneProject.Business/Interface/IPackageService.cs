using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Calendar;
using CapstoneProject.DTO.Request.Package;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Calendar;
using CapstoneProject.DTO.Response.Package;

namespace CapstoneProject.Business.Interface
{
    public interface IPackageService
    {
        Task<BaseListResponse<PackageResponse>> GetList(ListRequest request);
        Task<PackageResponse> GetById(string packageId);
        Task<PackageResponse> Create(PackageCreareRequest request);
        Task<PackageResponse> Update(PackageUpdateRequest request);
    }
}
