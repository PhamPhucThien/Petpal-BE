using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.Service
{
    public class CreateServiceRequest
    {

        [Required(ErrorMessage = "Service ID is required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Service Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Base Price is required")]
        public double BasePrice { get; set; }
        //[Required(ErrorMessage = "Is Required")]
        public bool IsRequired { get; set; }
    }
}
