using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.Blog
{
    public class BlogRequest
    {
        [Required(ErrorMessage = "Blog ID is required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Contents is required")]
        public string Contents { get; set; }
        //[Required(ErrorMessage = "Tags is required")]
        public string Tags { get; set; }


    }
}
