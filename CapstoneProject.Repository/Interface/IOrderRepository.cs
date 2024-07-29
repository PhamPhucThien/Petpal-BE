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
        Task<List<Order>?> GetByManagerId(Guid userId, Paging paging);
        Task<List<Order>?> GetByPartnerId(Guid userId, Paging paging);
        Task<List<Order>?> GetByUserId(Guid userId, Paging paging);
    }
}
