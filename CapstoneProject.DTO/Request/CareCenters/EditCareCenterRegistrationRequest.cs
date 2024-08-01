using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.CareCenters
{
    public class EditCareCenterRegistrationRequest
    {
        [Required]
        public Guid CareCenterId { get; set; }
    }
}
