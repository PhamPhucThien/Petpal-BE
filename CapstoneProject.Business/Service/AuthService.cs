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
    public class AuthService(IAuthRepository authRepository) : IAuthService
    {
        public IAuthRepository authRepository = authRepository;

        public async Task<User> Login(LoginRequest request)
        {
            User? user = await authRepository.GetByUsernameAndPassword(request.Username, request.Password);
            return user != null ? user : new User();
        }
    }
}
