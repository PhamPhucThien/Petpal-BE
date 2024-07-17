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
    public class OrderRepository : RepositoryGeneric<Order>, IOrderRepository
    {
        public OrderRepository(DbContextOptions<PetpalDbContext> contextOptions) : base(contextOptions)
        {
        }
    }
}
