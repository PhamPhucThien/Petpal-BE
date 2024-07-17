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
    public class ServiceRepository : RepositoryGeneric<Service>, IServiceRepository
    {
        public ServiceRepository(DbContextOptions<PetpalDbContext> contextOptions) : base(contextOptions)
        {
        }
    }
}
