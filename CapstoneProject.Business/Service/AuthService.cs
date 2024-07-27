using CapstoneProject.Business.Interface;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Account;
using CapstoneProject.DTO.Response.Account;
using CapstoneProject.Repository.Interface;


namespace CapstoneProject.Business.Service
{
    public class AuthService(IAuthRepository authRepository, IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository) : IAuthService
    {
        private readonly IAuthRepository _authRepository = authRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
        private readonly IUserRepository _userRepository = userRepository;
        public StatusCode StatusCode { get; set; } = new StatusCode();
        public async Task<ResponseObject<LoginResponse>> Login(LoginRequest request)
        {
            ResponseObject<LoginResponse> response = new();
            LoginResponse data = new();
            User? user = await _authRepository.GetByUsernameAndPassword(request.Username, request.Password);
            if (user == null)
            {
                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Sai tài khoản hoặc mật khẩu, xin vui lòng nhập lại.";
                response.Payload.Data = null;

            }
            else
            {
                response.Status = StatusCode.OK;
                response.Payload.Message = "Đăng nhập thành công";

                data.Id = user.Id;
                data.Role = user.Role;
                data.Name = user.FullName;
                data.Token = _jwtTokenGenerator.GenerateToken(data.Id, data.Role);
                response.Payload.Data = data;

            }
            return response;
        }

        public async Task<ResponseObject<LoginResponse>> Register(RegisterRequest request)
        {
            ResponseObject<LoginResponse> response = new();
            LoginResponse data = new();
            User? user = await _authRepository.GetByUsername(request.Username);
            if (user != null)
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Đã tồn tại tên tài khoản, xin vui lòng nhập lại";
                response.Payload.Data = null;
            }
            else
            {
                response.Status = StatusCode.OK;
                response.Payload.Message = "Tạo tài khoản thành công";

                User newUser = new()
                {
                    Id = Guid.NewGuid(),
                    Username = request.Username,
                    Password = request.Password,
                    FullName = request.FullName,
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    Role = Database.Model.Meta.UserRole.CUSTOMER,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = request.Username
                };

                User? checkUser = await _userRepository.AddAsync(newUser);

                if (checkUser != null)
                {
                    data.Id = newUser.Id;
                    data.Role = newUser.Role;
                    data.Name = newUser.FullName;
                    data.Token = _jwtTokenGenerator.GenerateToken(data.Id, data.Role);
                    response.Payload.Data = data;
                }
                else
                {
                    response.Status = StatusCode.BadRequest;
                    response.Payload.Message = "Không thể tạo tài khoản";
                    response.Payload.Data = null;
                }

            }
            return response;
        }
    }
}
