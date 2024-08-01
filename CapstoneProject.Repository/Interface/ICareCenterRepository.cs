using CapstoneProject.Database.Model;
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
        Task<CareCenter?> GetByPartnerId(Guid partnerId);
        Task<CareCenter?> GetCareCenterByIdAsync(Guid careCenterId);
    }
}
