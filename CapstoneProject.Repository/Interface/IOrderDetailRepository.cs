using CapstoneProject.Database.Model;
using CapstoneProject.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Repository.Interface
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        Task<OrderDetail?> GetByIdAsyncCustom(Guid petId);
        Task<int> CountByPetIdAsyncCustom(Guid petId);
    }
}
