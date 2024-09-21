using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Response.Dashboard
{
    public class PartnerDashboard
    {
        public int CareCenters { get; set; }
        public int Orders { get; set; }
        public int Customers { get; set; }
        public int UsingPackages { get; set; }
    }
}
