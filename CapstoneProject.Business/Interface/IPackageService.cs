using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Package;
using CapstoneProject.DTO.Response.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Business.Interface
{
    public interface IPackageService
    {
        Task<ResponseObject<PackageResponse>> GetById(GetPackageByIdRequest request);
        Task<ResponseObject<ListPackageResponse>> GetListByCareCenterId(GetListPackageByCareCenterIdRequest request);
    }
}
