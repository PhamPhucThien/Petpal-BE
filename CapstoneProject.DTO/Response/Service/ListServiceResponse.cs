using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Response.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Response.Service
{
    public class ListServiceResponse
    {
        public List<ServiceResponseModel> List { get; set; } = [];
        public Paging Paging { get; set; } = new();
    }

    public class ServiceResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
