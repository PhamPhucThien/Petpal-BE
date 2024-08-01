namespace CapstoneProject.DTO.Response.Base;

public class BaseResponse
{
    public Guid Id { get; set; }
    public string? Status { get; set; }
    public string? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}