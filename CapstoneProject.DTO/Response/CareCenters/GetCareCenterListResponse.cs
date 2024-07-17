using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Response.CareCenters
{
    public class GetCareCenterListResponse
    {
        public List<CareCenter>? List { get; set; }
        public Paging Paging { get; set; } = new Paging();
    }
}
