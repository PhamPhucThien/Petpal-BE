using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Response.User
{
    public class ListUserResponse
    {
        public List<UserModel> List { get; set; } = [];
        public Paging Paging { get; set; } = new();
    }

    public class UserModel
    {
        public Guid? Id { get; set; }
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public UserRole? Role { get; set; }
    }
}
