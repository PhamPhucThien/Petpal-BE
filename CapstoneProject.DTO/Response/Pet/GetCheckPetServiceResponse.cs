using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Response.Pet
{
    public class GetCheckPetServiceResponse
    {
        public List<CheckPetModel> List = [];
    }

    public class CheckPetModel
    {
        public Guid? Id { get; set; }
        public string? FullName { get; set; }
        public bool IsChecked { get; set; }
    }
}
