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
    public interface IServiceRepository : IRepository<Service>
    {
        Task<List<Service>> GetByListIdsAsync(List<Guid> listIds, string creator);
        Task<Service?> GetByUsernameAndName(string? username, string name);
        Task<Tuple<List<Service>, int>> GetWithPagingCustom(Paging paging, string creator);
    }
}
