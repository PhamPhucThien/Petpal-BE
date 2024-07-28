using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Response.Package
{
    public class PackageItemResponse
    {
        public Guid Id { get; set; }
        public double CurrentPrice { get; set; }
        public string? Detail { get; set; }
    }
}
