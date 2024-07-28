using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Response.Base;

namespace CapstoneProject.DTO.Response.Package
{
    public class PackageResponse : BaseResponse
    { 
        public string Description { get; set; }
        public string Duration { get; set; }
        public string Type { get; set; }
        public double TotalPrice { get; set; }
        public CareCenter CareCenter { get; set; }
    }
}
