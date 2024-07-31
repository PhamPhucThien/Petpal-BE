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
        public User GetUserByUsername(string username);
        Task<List<User>?> GetWithPagingAndStatusAndRole(Paging paging, UserStatus? status, UserRole? role);
    }
}
