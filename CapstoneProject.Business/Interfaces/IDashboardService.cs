using CapstoneProject.DTO;
using CapstoneProject.DTO.Response.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Business.Interfaces
{
    public interface IDashboardService
    {
        Task<ResponseObject<AdminDashboard>> GetDashBoardAdmin();
        Task<ResponseObject<PartnerDashboard>> GetDashBoardPartner(Guid userId);
    }
}
