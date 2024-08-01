using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Account;
using CapstoneProject.DTO.Request.User;
using CapstoneProject.DTO.Response.Account;

namespace CapstoneProject.Business.Interface
{
    public interface IAuthService
    {
        Task<ResponseObject<LoginResponse>> Login(LoginRequest request);
        Task<ResponseObject<LoginResponse>> Register(RegisterRequest request);
        Task<ResponseObject<LoginResponse>> RegisterPartner(CreatePartnerRequest request);
    }
}
