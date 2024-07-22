using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.Package
{
    public class PackageItemUpdateRequest
    {
        [Required(ErrorMessage = "Item ID is required")]
        public string Id { get; set; }
        public string? PackageId { get; set; }
        [Required(ErrorMessage = "Service ID is required")]
        public string ServiceId { get; set; }
        [Required(ErrorMessage = "CurrentPrice is required")]
        public double CurrentPrice { get; set; }
        [Required(ErrorMessage = "Detail is required")]
        public string Detail { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
        [Required(ErrorMessage = "UpdatedBy is required")]
        public string UpdatedBy { get; set; }
    }
}
