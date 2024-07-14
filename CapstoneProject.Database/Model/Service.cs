using CapstoneProject.Database.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model
{
    public class Service : BaseModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? BasePrice { get; set; }
        public bool? IsRequired { get; set; }
    }
}
