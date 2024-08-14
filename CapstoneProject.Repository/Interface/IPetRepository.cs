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
    public interface IPetRepository : IRepository<Pet>
    {
        Task<List<Pet>?> GetActiveByUserId(Guid userId, Paging paging);
        Task<List<Pet>?> GetByUserId(Guid userId, Paging paging);
    }
}
