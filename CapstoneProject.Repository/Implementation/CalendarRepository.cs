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
using CapstoneProject.DTO.Request;

namespace CapstoneProject.Repository.Repository
{
    public class CalendarRepository(DbContextOptions<PetpalDbContext> contextOptions) : RepositoryGeneric<Calendar>(contextOptions), ICalendarRepository
    {
        private PetpalDbContext _dbContext;

        public async Task<List<Calendar>> GetWithPaging(Paging paging)
        {
            if (paging == null)
            {
                throw new ArgumentNullException(nameof(paging));
            }
            
            IQueryable<Calendar> query = _dbContext.Set<Calendar>() 
                    .Include(o => o.CareCenter)
                    .AsQueryable()
                ;

            query = query.Skip(paging.Size * (paging.Page - 1))
                .Take(paging.Size);

            return await query.ToListAsync();
        }

        public async Task<Calendar?> GetByIdAsync(Guid id)
        {
            return _dbContext.Calendars.AsNoTracking().Where(o => o.Id.Equals(id))
                .Include(o => o.CareCenter)
                .FirstOrDefault();
        }
    }
}
