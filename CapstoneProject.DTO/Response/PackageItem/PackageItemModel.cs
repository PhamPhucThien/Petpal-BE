using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Response.PackageItem
{
    public class PackageItemModel
    {
        public Guid ServiceId { get; set; }
        public double CurrentPrice { get; set; }
        public string? Detail { get; set; }
    }
}
