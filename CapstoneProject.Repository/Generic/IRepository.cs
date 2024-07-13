using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Repository.Generic
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<T> AddAsync(T entity);
        Task<bool> EditAsync(T entity);
        Task<bool> DeleteByIdAsync(Guid id);
/*        IEnumerable<T> GetWithPaging(IPagingRequest pagingRequest);
*/    }
}
