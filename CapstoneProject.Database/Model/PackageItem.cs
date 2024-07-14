using CapstoneProject.Database.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model
{
    public class PackageItem : BaseModel
    {
        public Guid? PackageId { get; set; }
        public Guid? ServiceId { get; set; }
        public double CurrentPrice { get; set; }
        public string? Detail { get; set; }

        public Package? Package { get; set; }
        public Service Service { get; set; }
    }
}
