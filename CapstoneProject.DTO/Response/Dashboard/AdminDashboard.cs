using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Response.Dashboard
{
    public class AdminDashboard
    {
        public int Users {  get; set; }
        public int Partners { get; set; }
        public int CareCenters { get; set; }
        public int Invoices { get; set; }
    }
}
