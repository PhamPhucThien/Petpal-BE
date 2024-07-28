using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Response.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Response.Package
{
    public class ListPackageResponse
    {
        public List<PackageResponseModel> Packages { get; set; } = [];
        public Paging Paging { get; set; } = new();
    }

    public class PackageResponseModel
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public string? Duration { get; set; }
        public string? Type { get; set; }
        public double TotalPrice { get; set; }
        public List<ListPackageItemResponseModel>? Items { get; set; } = [];
    }

    public class ListPackageItemResponseModel
    {
        public Guid Id { get; set; }
        public double CurrentPrice { get; set; }
        public string? Detail { get; set; }
    }
}
