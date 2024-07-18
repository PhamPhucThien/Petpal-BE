using CapstoneProject.DTO.Request;

namespace CapstoneProject.DTO.Response.Pet;

public class PetListResponse
{
    public List<PetResponse>? List { get; set; }
    public Paging Paging { get; set; } = new Paging();
}