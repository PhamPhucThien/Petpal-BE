using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.DTO.Request.Comment;

public class CommentCreateRequest
{
    public string? RelatedId { get; set; }
    public string? CommentId { get; set; }
    [Required(ErrorMessage = "UserID is required")]
    public string UserId { get; set; }
    [Required(ErrorMessage = "UserID is required")]
    public string Content { get; set; }
    [Required(ErrorMessage = "UserID is required")]
    public int LikeNumber { get; set; }
    [Required(ErrorMessage = "UserID is required")]
    public string CreatedBy { get; set; }
}