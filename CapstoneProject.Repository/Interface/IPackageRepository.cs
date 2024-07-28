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
    public interface IPackageRepository : IRepository<Package>
    {
        Task<List<Package>> GetWithPagingByCareCenterId(Guid careCenterId, Paging paging);
        Task<Package?> GetByIdIncludePackageItem(Guid packageId);
    }
}
