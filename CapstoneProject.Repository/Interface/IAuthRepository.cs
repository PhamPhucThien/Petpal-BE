using CapstoneProject.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Repository.Interface
{
    public interface IAuthRepository
    {
        Task<User?> GetByUsernameAndPassword(string username, string password);
    }
}
