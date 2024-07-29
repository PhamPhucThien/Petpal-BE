using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.Pet
{
    public class PetCreateRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Fullname { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }       
    }
}
