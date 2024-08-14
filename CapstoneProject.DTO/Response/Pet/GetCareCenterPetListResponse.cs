using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Response.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Response.Pet
{
    public class GetCareCenterPetListResponse
    {
        public List<GetCareCenterPetListModel> List { get; set; } = [];

        public Paging Paging { get; set; } = new();
    }

    public class GetCareCenterPetListModel
    {
        public PackageResponseModel Model { get; set; } = new();
        public List<PetModel> Pet { get; set; } = [];
    }
}
