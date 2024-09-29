using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.Pet
{
    public class CheckPetServiceRequest
    {
        [Required]
        public Guid PackageItemId { get; set; }
        [Required]
        public List<Guid> PetIds { get; set; } = [];
    }
}
