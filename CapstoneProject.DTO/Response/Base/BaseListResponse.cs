using CapstoneProject.DTO.Request;

namespace CapstoneProject.DTO.Response.Base;

public class BaseListResponse<T>
{
    public List<T> List { get; set; }
    public Paging Paging { get; set; } = new Paging();
}