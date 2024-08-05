using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO.Request.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.User
{
    public class ListUserRequest
    {
        [Required]
        public ListRequest ListRequest { get; set; } = new();
        public UserRole? Role { get; set; }
        public UserStatus? Status { get; set; }
    }
}
