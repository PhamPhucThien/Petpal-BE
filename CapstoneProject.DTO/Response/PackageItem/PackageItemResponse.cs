using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Service;

namespace CapstoneProject.DTO.Response.Package
{
    public class ListPackageItemResponse : BaseResponse
    {
        public double CurrentPrice { get; set; }
        public string Detail { get; set; }
        public PackageResponse Package { get; set; }
        public ServiceResponse Service { get; set; }
    }
}
