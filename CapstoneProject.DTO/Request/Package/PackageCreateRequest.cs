using CapstoneProject.DTO.Response.PackageItem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.Package
{
    public class PackageCreateRequest
    {
        public string? Description { get; set; }
        public string? Type { get; set; }
        public string? Title { get; set; }
        public Guid? PetTypeId { get; set; }
        public List<PackageItemModel> PackageItems { get; set; } = [];
    }
}
