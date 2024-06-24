using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Response.Pet
{
    public class PetResponse
    {
        
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public int PetTypeId { get; set; }
        
        public string PetName { get; set; }
      
        public string Description { get; set; }

    }
}
