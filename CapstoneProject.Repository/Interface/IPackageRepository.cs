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
        Task<Tuple<List<Package>, int>> GetWithPagingByCareCenterId(Guid careCenterId, Paging paging);
        Task<Package?> GetByIdIncludePackageItem(Guid packageId);
        Task<Tuple<List<Package>, int>> GetByCustomerId(Guid userId, Paging paging);
        Task<Tuple<List<Package>, int>> GetByStaffId(Guid userId, Paging paging);
        Task<Package?> GetByStaffIdAndPackageId(Guid userId, Guid packageId);
        Task<int> CountUsingPackageByPartnerId(Guid userId);
    }
}
