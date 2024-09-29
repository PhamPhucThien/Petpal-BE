using CapstoneProject.Database;
using CapstoneProject.Database.Model;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Repository.Repository
{
    public class AuthRepository(DbContextOptions<PetpalDbContext> contextOptions) : IAuthRepository
    {
        private readonly DbContextOptions<PetpalDbContext> _contextOptions = contextOptions;

        public async Task<User?> GetByUsernameAndPassword(string username, string password)
        {
            using PetpalDbContext context = new(_contextOptions);
            User? user = await context.Set<User>().FirstOrDefaultAsync(x => x.Username == username && x.Password == password && x.Status == UserStatus.ACTIVE);
            return user;
        }

        public async Task<User?> GetByUsername(string username)
        {
            using PetpalDbContext context = new(_contextOptions);
            User? user = await context.Set<User>().FirstOrDefaultAsync(x => x.Username == username);
            return user;

        }

        public async Task<User?> GetByEmail(string? email)
        {
            using PetpalDbContext context = new(_contextOptions);
            User? user = await context.Set<User>().FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }
    }
}
