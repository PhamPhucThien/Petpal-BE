using CapstoneProject.Database.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model
{
    public class PetType : BaseModel
    {
        public string? Type { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
    }
}
