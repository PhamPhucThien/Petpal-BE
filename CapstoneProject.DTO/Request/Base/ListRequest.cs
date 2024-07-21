using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.DTO.Request.Base;

public class ListRequest
{
    [Range(1, Int64.MaxValue)]
    public int Page { get; set; }
    [Range(1, 20)]
    public int Size { get; set; }
}