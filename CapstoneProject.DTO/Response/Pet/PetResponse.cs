using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Response.User;

namespace CapstoneProject.DTO.Response.Pet
{
    public class PetResponse
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public string Description { get; set; }
        public UserResponse User { get; set; }
        public PetType PetType { get; set; }
        public string Status { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
