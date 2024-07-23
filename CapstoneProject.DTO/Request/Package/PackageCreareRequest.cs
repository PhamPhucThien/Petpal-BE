using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.Package
{
    public class PackageCreareRequest
    {
        [Required(ErrorMessage = "Care Center ID is required")]
        public string CareCenterId { get; set; }
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Duration is required")]
        public string Duration { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "TotalPrice is required")]
        public double TotalPrice { get; set; }
        [Required(ErrorMessage = "CreatedBy is required")]
        public string CreatedBy { get; set; }
    }
}
