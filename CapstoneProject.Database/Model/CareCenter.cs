using CapstoneProject.Database.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model
{
    public class CareCenter : BaseModel
    {
        public Guid? PartnerId { get; set; }
        public Guid? ManagerId { get; set; }
        public string? CareCenterName { get; set; }
        public string? ListImages { get; set; }
        public string? Address { get; set; }
        public string? Descriptionn { get; set; }
        public string? Lattitude { get; set; }
        public string? Longtitude { get; set; }
        public float AverageRating { get; set; }

        public User? Partner { get; set; }
        public User? Manager { get; set; }
    }
}
