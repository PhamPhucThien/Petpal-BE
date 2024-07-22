using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request.Package;
using CapstoneProject.DTO.Response.Package;
using CapstoneProject.DTO.Response.Service;

namespace CapstoneProject.Infrastructure.Profile;

public class PackageItemProfile : AutoMapper.Profile
{
    public PackageItemProfile()
    {
        CreateMap<PackageItem, PackageItemResponse>();
        CreateMap<PackageItemCreateRequest, PackageItem>();
        CreateMap<PackageItemUpdateRequest, PackageItem>();
        CreateMap<Package, PackageResponse>();
        CreateMap<Service, ServiceResponse>();
    }

}