using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.DTO.Request.Blog;

public class BlogUpdateRequest
{
    [Required(ErrorMessage = "Id is required")]
    public string Id { get; set; }
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Content is required")]
    public string Content { get; set; }
    [Required(ErrorMessage = "Tags is required")]
    public string Tags { get; set; }
    [Required(ErrorMessage = "ViewNumber is required")]
    public int ViewNumber { get; set; }
    [Required(ErrorMessage = "LikeNumber is required")]
    public int LikeNumber { get; set; }
    [Required(ErrorMessage = "Status is required")]
    public string Status { get; set; }
    [Required(ErrorMessage = "createBy is required")]
    public string UpdateBy { get; set; }
}