using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.DTO.Request.Comment;

public class CommentUpdateRequest
{
    [Required(ErrorMessage = "UserID is required")]
    public string Id { get; set; }
    public string? RelatedId { get; set; }
    public string? CommentId { get; set; }
    [Required(ErrorMessage = "UserID is required")]
    public string UserId { get; set; }
    [Required(ErrorMessage = "UserID is required")]
    public string Content { get; set; }
    [Required(ErrorMessage = "UserID is required")]
    public int LikeNumber { get; set; }
    [Required(ErrorMessage = "UserID is required")]
    public string Status { get; set; }
    [Required(ErrorMessage = "UserID is required")]
    public string UpdatedBy { get; set; }
}