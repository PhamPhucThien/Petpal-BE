using CapstoneProject.Database.Model;
using CapstoneProject.Database;
using CapstoneProject.Repository.Generic;
using CapstoneProject.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Repository.Implementation
{
    public class CareCenterStaffRepository(DbContextOptions<PetpalDbContext> contextOptions) : RepositoryGeneric<CareCenterStaff>(contextOptions), ICareCenterStaffRepository
    {
        private readonly DbContextOptions<PetpalDbContext> _contextOptions = contextOptions;
    }
}
