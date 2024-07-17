using CapstoneProject.Database.Model;
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
        public User GetUserByUsername(string username);
    }
}
