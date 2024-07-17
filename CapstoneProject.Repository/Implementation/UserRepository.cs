using CapstoneProject.Database;
using CapstoneProject.Database.Model;
using CapstoneProject.Repository.Generic;
using CapstoneProject.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Repository.Repository
{
    public class UserRepository : RepositoryGeneric<User>, IUserRepository
    {
        private PetpalDbContext _dbContext;
        public UserRepository(DbContextOptions<PetpalDbContext> contextOptions) : base(contextOptions)
        {
            _dbContext = new PetpalDbContext(contextOptions);
        }

        public User GetUserByUsername(string username)
        {
            return _dbContext.Users.Where(o => o.Username.Equals(username))
                .FirstOrDefault();
        }
    }
}
