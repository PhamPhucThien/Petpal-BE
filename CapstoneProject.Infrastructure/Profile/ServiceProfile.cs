using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request.Service;
using CapstoneProject.DTO.Response.Service;

namespace CapstoneProject.Infrastructure.Profile;

public class ServiceProfile : AutoMapper.Profile
{
    public ServiceProfile()
    {
        CreateMap<Service, ServiceResponse>();
        CreateMap<ServiceCreateRequest, Service>();
        CreateMap<ServiceUpdateRequest, Service>();
    }
}