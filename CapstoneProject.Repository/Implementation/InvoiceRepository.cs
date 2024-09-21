using CapstoneProject.Database;
using CapstoneProject.Database.Model;
using CapstoneProject.Database.Model.Meta;
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
    public class InvoiceRepository(DbContextOptions<PetpalDbContext> contextOptions) : RepositoryGeneric<Invoice>(contextOptions), IInvoiceRepository
    {
        private readonly DbContextOptions<PetpalDbContext> _contextOptions = contextOptions;

        public async Task<int> CountActiveInvoice()
        {
            using PetpalDbContext context = new(_contextOptions);
            int count = await context.Set<Invoice>().Where(x => x.Status == BaseStatus.ACTIVE).CountAsync();
            return count;
        }
    }
}
