using CapstoneProject.Database;
using CapstoneProject.Database.Model;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO.Request;
using CapstoneProject.Repository.Generic;
using CapstoneProject.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

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

        public async Task<Tuple<List<User>, int>> GetWithPagingAndStatusAndRole(Paging paging, UserStatus? status, UserRole? role)
        {
            ArgumentNullException.ThrowIfNull(paging);

            using PetpalDbContext context = new(_contextOptions);
            IQueryable<User> query = context.Set<User>().AsQueryable();

            int count = await query.CountAsync();

            count = count % paging.Size == 0 ? count / paging.Size : count / paging.Size + 1;

            if (paging.Size <= 0) { paging.Size = 1; }

            if (status != null)
            {
                query = query.Where(o => o.Status == status);
            }

            if (role != null)
            {
                query = query.Where(o => o.Role == role);
            }
            if (paging.Search != string.Empty)
            {
                query = query.Where(o => (o.Username != null && o.Username.Contains(paging.Search)) || (o.FullName != null && o.FullName.Contains(paging.Search)));
            }

            query = query.Skip(paging.Size * (paging.Page - 1))
                         .Take(paging.Size);

            List<User> users = await query.ToListAsync();

            Tuple<List<User>, int> data = new(users, count);

            return data;
        }
    }
}
