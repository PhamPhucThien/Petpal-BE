using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.Pet
{
    public class UpdatePetRequest
    {
        [Required(ErrorMessage = "ID is required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Type ID is required")]
        public int PetTypeId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string PetName { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}
