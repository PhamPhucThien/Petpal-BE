using CapstoneProject.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Response.Package
{
    public class ListPackageResponse
    {
        public List<PackageResponse> Packages { get; set; } = [];
        public Paging Paging { get; set; } = new();
    }
}
