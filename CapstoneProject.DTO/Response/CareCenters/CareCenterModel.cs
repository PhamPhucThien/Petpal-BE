using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Response.CareCenters
{
    public class CareCenterModel
    {
        public string? CareCenterName { get; set; }
        public string? ListImages { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public double AverageRating { get; set; }
    }
}
