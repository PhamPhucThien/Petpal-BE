using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request.Pet;
using CapstoneProject.DTO.Request.PetType;
using CapstoneProject.DTO.Response.Pet;
using CapstoneProject.DTO.Response.PetType;
using CapstoneProject.DTO.Response.User;

namespace CapstoneProject.Infrastructure.Profile;

public class PetTypeProfile : AutoMapper.Profile
{
    public PetTypeProfile()
    {
        CreateMap<PetType, PetTypeDetailResponse>();
        CreateMap<PetTypeCreateRequest, PetType>();
        CreateMap<PetTypeUpdateRequest, PetType>();
    }
}