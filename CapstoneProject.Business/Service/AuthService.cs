using CapstoneProject.Business.Interface;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Account;
using CapstoneProject.DTO.Request.User;
using CapstoneProject.DTO.Response.Account;
using CapstoneProject.Repository.Interface;
using CapstoneProject.Repository.Repository;
using System.Transactions;


namespace CapstoneProject.Business.Service
{
    public class AuthService(IAuthRepository authRepository, IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, ICareCenterRepository careCenterRepository) : IAuthService
    {
        private readonly IAuthRepository _authRepository = authRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICareCenterRepository _careCenterRepository = careCenterRepository;
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

        public async Task<ResponseObject<LoginResponse>> RegisterPartner(CreatePartnerRequest request)
        {
            ResponseObject<LoginResponse> response = new();
            LoginResponse data = new();

            User? partner = await _authRepository.GetByUsername(request.Partner.Username);
            User? manager = await _authRepository.GetByUsername(request.Manager.Username);

            if (partner != null)
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Đã tồn tại tên tài khoản của đối tác, xin vui lòng nhập lại";
                response.Payload.Data = null;
            }
            else if (manager != null)
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Đã tồn tại tên tài khoản của quản lý, xin vui lòng nhập lại";
                response.Payload.Data = null;
            }
            else
            {
                response.Status = StatusCode.OK;
                response.Payload.Message = "Tạo tài khoản thành công";

                using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                User newPartner = new()
                {
                    Id = Guid.NewGuid(),
                    Username = request.Partner.Username,
                    Password = request.Partner.Password,
                    FullName = request.Partner.FullName,
                    Address = request.Partner.Address,
                    PhoneNumber = request.Partner.PhoneNumber,
                    Email = request.Partner.Email,
                    Role = Database.Model.Meta.UserRole.PARTNER,
                    Status = Database.Model.Meta.UserStatus.PENDING,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = request.Partner.Username
                };

                User newManager = new()
                {
                    Id = Guid.NewGuid(),
                    Username = request.Manager.Username,
                    Password = request.Manager.Password,
                    FullName = "",
                    Role = Database.Model.Meta.UserRole.MANAGER,
                    Status = Database.Model.Meta.UserStatus.PENDING,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = request.Partner.Username
                };

                CareCenter newCareCenter = new()
                {
                    Id = Guid.NewGuid(),
                    PartnerId = newPartner.Id, 
                    ManagerId = newManager.Id,
                    CareCenterName = request.CareCenter.CareCenterName,
                    Address = request.CareCenter.Address,
                    Description = request.CareCenter.Description,
                    Status = Database.Model.Meta.CareCenterStatus.PENDING,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = request.Partner.Username
                };

                User? checkUser = await _userRepository.AddAsync(newPartner);
                _ = await _userRepository.AddAsync(newManager);
                _ = await _careCenterRepository.AddAsync(newCareCenter);

                if (checkUser != null)
                {
                    data.Id = newPartner.Id;
                    data.Role = newPartner.Role;
                    data.Name = newPartner.FullName;
                    data.Token = _jwtTokenGenerator.GenerateToken(data.Id, data.Role);
                    response.Payload.Data = data;
                }
                else
                {
                    response.Status = StatusCode.BadRequest;
                    response.Payload.Message = "Không thể tạo tài khoản";
                    response.Payload.Data = null;
                }

                scope.Complete();

            }
            return response;
        }
    }
}
