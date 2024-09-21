using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Response.Pet
{
    public class PetModel
    {
        public Guid? Id { get; set; }
        public string? FullName { get; set; }
        public string? ProfileImage { get; set; }
        public DateTimeOffset? Birthday { get; set; }
        public double? Weight { get; set; }
        public Gender? Gender { get; set; }
        public string? Breed { get; set; }
        public bool? Sterilise { get; set; }
        public string? Description { get; set; }
        public PetStatus Status { get; set; }
        public bool? IsCheckIn { get; set; }
        public bool? IsCheckOut { get; set; }
        public string? CheckInImg { get; set; }
        public string? CheckOutImg { get; set; }
    }

    public class ListPetModel
    {
        public List<PetModel> List { get; set; } = [];
        public Paging Paging { get; set; } = new();
    }
}
