using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.Package
{
    public class PackageUpdateRequest
    {
        [Required(ErrorMessage = "Package ID is required")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Care Center ID is required")] 
        public string CareCenterId { get; set; }
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Duration is required")]
        public string Duration { get; set; }
        [Required(ErrorMessage = "Detail is required")]
        public string Detail { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
        [Required(ErrorMessage = "UpdatedBy is required")]
        public string UpdatedBy { get; set; }
    }
}
