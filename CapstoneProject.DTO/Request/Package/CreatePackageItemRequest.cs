using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.Package
{
    public class CreatePackageItemRequest
    {
        [Required(ErrorMessage = "Item ID is required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Package ID is required")]
        public int PackageId { get; set; }
        [Required(ErrorMessage = "Service ID is required")]
        public int ServiceId { get; set; }
        [Required(ErrorMessage = "CurrentPrice is required")]
        public double CurrentPrice { get; set; }

        [Required(ErrorMessage = "Detail is required")]
        public string Detail { get; set; }


        //[Required(ErrorMessage = "Status is required")]
        //public string Status { get; set; }
    }
}
