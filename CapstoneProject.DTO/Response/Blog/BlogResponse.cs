using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.User;

namespace CapstoneProject.DTO.Response.Blog
{
    public class BlogResponse : BaseResponse
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public int ViewNumber { get; set; }
        public int LikeNumber { get; set; }
        public string ListImages { get; set; }
        public UserInforResponse User { get; set; }
    }
}
