using CapstoneProject.Database;
using CapstoneProject.Database.Model;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO.Request;
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
    public class UserRepository(DbContextOptions<PetpalDbContext> contextOptions) : RepositoryGeneric<User>(contextOptions), IUserRepository
    {
        private readonly DbContextOptions<PetpalDbContext> _contextOptions = contextOptions;

        public async Task<int> Count()
        {
            using PetpalDbContext context = new(_contextOptions);
            int count = await context.Set<User>().CountAsync();
            return count;
        }

        public User GetUserByUsername(string username)
        {
            using PetpalDbContext context = new(_contextOptions);

            return context.Users.Where(o => o.Username.Equals(username))
                .FirstOrDefault();
        }

        public async Task<List<User>?> GetWithPagingAndStatusAndRole(Paging paging, UserStatus? status, UserRole? role)
        {
            ArgumentNullException.ThrowIfNull(paging);

            using PetpalDbContext context = new(_contextOptions);
            IQueryable<User> query = context.Set<User>().AsQueryable();

            if (status != null)
            {
                query = query.Where(o => o.Status == status);
            }

            if (role != null)
            {
                query = query.Where(o => o.Role == role);
            }

            query = query.Skip(paging.Size * (paging.Page - 1))
                         .Take(paging.Size);

            return await query.ToListAsync();
        }
    }
}
