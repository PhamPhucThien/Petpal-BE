using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneProject.DTO.Response.Base;

namespace CapstoneProject.DTO.Response.Service
{
    public class ServiceResponse : BaseResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double BasePrice { get; set; }
        public bool IsRequired { get; set; }
    }
}
