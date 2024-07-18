using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request.Pet;
using CapstoneProject.DTO.Response.Pet;
using CapstoneProject.DTO.Response.User;

namespace CapstoneProject.Infrastructure.Profile;

public class PetProfile : AutoMapper.Profile
{
    public PetProfile()
    {
        CreateMap<Pet, PetResponse>();
        CreateMap<User, UserResponse>();
        CreateMap<PetCreateRequest, Pet>();
        CreateMap<PetUpdateRequest, Pet>();
    }
}