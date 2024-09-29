using CapstoneProject.Database.Model.Meta;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.Pet
{
    public class PetUpdateRequest
    {
        [Required]
        public Guid PetId { get; set; }
        public string? Fullname { get; set; }
        public Guid PetTypeId { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset? Birthday { get; set; }
        public double? Weight { get; set; }
        public Gender Gender { get; set; }
        public string? Breed { get; set; }
        public bool? Sterilise { get; set; }
    }
}
