
using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request.Package;
using CapstoneProject.DTO.Response.Package;

namespace CapstoneProject.Infrastructure.Profile;

public class PackageProfile : AutoMapper.Profile
{
    public PackageProfile()
    {
        CreateMap<Package, PackageResponse>();
        CreateMap<PackageCreareRequest, Package>();
        CreateMap<PackageUpdateRequest, Package>();
        CreateMap<Package, PackageResponseModel>();
    }
}