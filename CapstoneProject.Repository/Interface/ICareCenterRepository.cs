using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request;
using CapstoneProject.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Repository.Interface
{
    public interface ICareCenterRepository : IRepository<CareCenter>
    {
        Task<int> CountActiveCareCenter();
        Task<int> CountActiveCareCenterByPartnerId(Guid userId);
        Task<CareCenter?> GetByManagerId(Guid managerId);
        Task<Tuple<List<CareCenter>, int>> GetByPartnerId(Guid partnerId, Paging paging);
        Task<CareCenter?> GetCareCenterByIdAsync(Guid careCenterId);
        Task<Tuple<List<CareCenter>, int>> GetWithPagingCustom(Paging paging);
    }
}
