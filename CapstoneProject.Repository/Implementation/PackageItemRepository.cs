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
    public class PackageItemRepository : RepositoryGeneric<PackageItem>, IPackageItemRepository
    {
        public PackageItemRepository(DbContextOptions<PetpalDbContext> contextOptions) : base(contextOptions)
        {
        }
    }
}
