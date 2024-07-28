using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Response.Package
{
    public class PackageResponse
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public string? Duration { get; set; }
        public string? Type { get; set; }
        public double TotalPrice { get; set; }
        public List<PackageItemResponse>? Items { get; set; } = [];
    }
}
