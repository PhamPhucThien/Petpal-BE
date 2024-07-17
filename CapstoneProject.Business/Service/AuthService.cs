using CapstoneProject.Business.Interface;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request.Account;
using CapstoneProject.Repository.Interface;


namespace CapstoneProject.Business.Service
{
    public class AuthService(IAuthRepository authRepository) : IAuthService
    {
        public IAuthRepository _authRepository = authRepository;

        public async Task<User> Login(LoginRequest request)
        {
            User? user = await _authRepository.GetByUsernameAndPassword(request.Username, request.Password);
            return user != null ? user : new User();
        }
    }
}
