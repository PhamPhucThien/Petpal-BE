using CapstoneProject.Database.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model
{
    public class Calendar : BaseModel
    {
        public Guid? CareCenterId { get; set; }
        public string? PetAmountList { get; set; }
        public string? Year { get; set; }

        public CareCenter? CareCenter { get; set; }
    }
}
