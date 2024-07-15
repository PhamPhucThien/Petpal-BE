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
    public class AuthRepository : IAuthRepository
    {
        private readonly DbContextOptions<PetpalDbContext> _contextOptions;
        
        public async Task<User?> GetByUsernameAndPassword(string username, string password)
        {
            using PetpalDbContext context = new(_contextOptions);
            User? users = await context.Set<User>().FirstOrDefaultAsync(x => x.Username == username && x.Password == password && x.Status == UserStatus.ACTIVE);
        }
    }
}
