using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.PetType;
using CapstoneProject.DTO.Response.User;

namespace CapstoneProject.DTO.Response.Pet
{
    public class PetResponse : BaseResponse
    {
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public string Description { get; set; }
        public UserInforResponse User { get; set; }
        public PetTypeInforResponse PetType { get; set; }
    }
}
