using CapstoneProject.Business.Interface;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request.Account;
using CapstoneProject.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Business.Service
{
    public class AuthService(IUserRepository userRepository) : IAuthService
    {
        public IUserRepository userRepository = userRepository;

        public async Task<List<User>> Login(LoginRequest request)
        {
            List<User> user = (List<User>)await userRepository.GetAll();
            return user;
        }
    }
}
