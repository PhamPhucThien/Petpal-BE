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
    public interface IOrderRepository : IRepository<Order>
    {
        Task<int> Count();
        Task<double?> CountMoney();
        Task<Tuple<List<Order>, int>> GetByManagerId(Guid userId, Paging paging);
        Task<Tuple<List<Order>, int>> GetByPartnerId(Guid userId, Paging paging);
        Task<Tuple<List<Order>, int>> GetByUserId(Guid userId, Paging paging);
        Task<Order?> GetByOrderId(Guid id);
        Task<Tuple<List<Order>, int>> GetPendingByManagerId(Guid userId, Paging paging);
        Task<int> CountOrderByPartnerId(Guid userId);
    }
}
