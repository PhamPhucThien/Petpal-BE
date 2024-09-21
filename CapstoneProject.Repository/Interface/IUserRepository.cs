using CapstoneProject.Database.Model;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO.Request;
using CapstoneProject.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Repository.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<int> Count();
        Task<int> CountActivePartner();
        Task<int> CountActiveUser();
        Task<int> CountCustomerByPartnerId(Guid userId);
        public User GetUserByUsername(string username);
        Task<Tuple<List<User>, int>> GetWithPagingAndStatusAndRole(Paging paging, UserStatus? status, UserRole? role);
    }
}
