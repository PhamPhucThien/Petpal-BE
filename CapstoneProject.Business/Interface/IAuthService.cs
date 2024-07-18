using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request.Account;

namespace CapstoneProject.Business.Interface
{
    public interface IAuthService
    {
        Task<User?> Login(LoginRequest request);
        Task<bool> Register(RegisterRequest request);
    }
}
