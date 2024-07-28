using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Response.CareCenters
{
    public class GetCareCenterListResponse
    {
        public List<CareCenterListModel> List { get; set; } = [];
        public Paging Paging { get; set; } = new Paging();
    }

    public class CareCenterListModel
    {
        public Guid Id { get; set; }
        public string? CareCenterName { get; set; }
        public string? ListImages { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public double AverageRating { get; set; }
    }
}
