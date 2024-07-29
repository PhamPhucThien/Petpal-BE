using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Response.Pet
{
    public class PetModel
    {
        public Guid? Id { get; set; }
        public string? FullName { get; set; }
        public string? ProfileImage { get; set; }
        public string? Description { get; set; }
    }
}
