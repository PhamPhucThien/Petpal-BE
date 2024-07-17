using CapstoneProject.Database.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.CareCenters
{
    public class GetCareCenterListRequest
    {
        [Range(1, Int64.MaxValue)]
        public int Page { get; set; }
        [Range(1, 20)]
        public int Size { get; set; }
    }
}
