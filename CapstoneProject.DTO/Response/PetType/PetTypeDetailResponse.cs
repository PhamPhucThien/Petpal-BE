using CapstoneProject.DTO.Response.Base;

namespace CapstoneProject.DTO.Response.PetType;

public class PetTypeDetailResponse : BaseResponse
{
    public string Type { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
}