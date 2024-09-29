using AutoMapper;
using CapstoneProject.Business.Interfaces;
using CapstoneProject.Database.Model;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.User;
using CapstoneProject.DTO.Response.Account;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.User;
using CapstoneProject.Repository.Interface;
using CapstoneProject.Repository.Repository;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Transactions;
using User = CapstoneProject.Database.Model.User;

namespace CapstoneProject.Business.Services
{
    public class UserService(ICareCenterStaffRepository careCenterStaffRepository, IAuthRepository authRepository, IUserRepository userRepository, ICareCenterRepository careCenterRepository, IMapper mapper) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IAuthRepository _authRepository = authRepository;
        private readonly ICareCenterStaffRepository _careCenterStaffRepository = careCenterStaffRepository;
        private readonly ICareCenterRepository _careCenterRepository = careCenterRepository;
        private readonly IMapper _mapper = mapper;
        public UploadImageService uploadImage = new();
        public StatusCode StatusCode { get; set; } = new();

        /* public Task<bool> UploadProfile(FileStream file)
         {
             *//*try
             {
                 var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey))
             } catch (Exception e)
             {

             }
             *//*
         }*/

        public async Task<BaseListResponse<UserDetailResponse>> GetList(ListRequest request)
        {
            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                Search = request.Search ?? string.Empty,
                MaxPage = 1
            };
            Tuple<List<User>, int> listUser = await _userRepository.GetWithPaging(paging);
            List<UserDetailResponse> listUserResponse = _mapper.Map<List<UserDetailResponse>>(listUser.Item1);
            paging.MaxPage = listUser.Item2;
            BaseListResponse<UserDetailResponse> response = new()
            {
                List = listUserResponse,
                Paging = paging,
            };
            return response;
        }

        public async Task<ResponseObject<UserDetailResponse>> GetUserById(Guid userId)
        {
            ResponseObject<UserDetailResponse> response = new();
            UserDetailResponse data = new();
            User? user = await _userRepository.GetByIdAsync(userId);

            if (user != null)
            {
                response.Status = StatusCode.OK;
                response.Payload.Message = "Truy vấn thông tin thành công";

                /*data.Username = user.Username;
                data.FullName = user.FullName;
                data.Address = user.Address;
                data.PhoneNumber = user.PhoneNumber;
                data.Email = user.Email;
                data.RoomId = user.RoomId;
                data.ProfileImage = user.ProfileImage;
                data.Role = user.Role.ToString();*/

                data = _mapper.Map<UserDetailResponse>(user);
                response.Payload.Data = data;
            } else
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Id người dùng không tồn tại";
                response.Payload.Data = null;
            }

            return response;

        }

        /*public async Task<ResponseObject<UserDetailResponse>> CreateUser(Guid userId, UserCreateRequest request)
        {
            ResponseObject<UserDetailResponse> response = new();
            UserDetailResponse data = new();
            User? user = await _userRepository.GetByIdAsync(userId);

            if (user != null)
            {
                user.FullName = request.Fullname ?? user.FullName;
                user.Address = request.Address ?? user.Address;
                user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
                user.RoomId = request.RoomId ?? user.RoomId;
                user.Email = request.Email ?? request.Email;

                user.UpdatedBy = user.Username;
                user.UpdatedAt = DateTimeOffset.Now;

                bool check = await _userRepository.EditAsync(user);

                if (check)
                {
                    response.Status = StatusCode.OK;
                    response.Payload.Message = "Thay đổi thông tin thành công";

                    data.Username = user.Username;
                    data.FullName = user.FullName;
                    data.Address = user.Address;
                    data.PhoneNumber = user.PhoneNumber;
                    data.Email = user.Email;
                    data.RoomId = user.RoomId;
                    data.ProfileImage = user.ProfileImage;
                    data.Role = user.Role.ToString();

                    response.Payload.Data = data;
                }
                else
                {
                    response.Status = StatusCode.BadRequest;
                    response.Payload.Message = "Thay đổi thông tin thất bại";
                    response.Payload.Data = null;
                }
            }

            return response;



            User userCheck = _userRepository.GetUserByUsername(request.Username);
            if (userCheck != null)
            {
                throw new Exception("Username is duplicated.");
            }
            User userCreate = _mapper.Map<User>(request);
            userCreate.Status = UserStatus.ACTIVE;
            userCreate.CreatedBy = request.CreatedBy;
            userCreate.CreatedAt = DateTimeOffset.Now;
            userCreate.Role = UserRole.CUSTOMER;
            User? user = await _userRepository.AddAsync(userCreate);
            return _mapper.Map<UserDetailResponse>(user);
        }*/

        public async Task<ResponseObject<UserDetailResponse>> UpdateUser(Guid userId, UserUpdateRequest request, FileDetails fileDetail)
        {
            ResponseObject<UserDetailResponse> response = new();
            UserDetailResponse data = new();
            User? user = await _userRepository.GetByIdAsync(userId);

            if (user != null)
            {
                user.FullName = request.Fullname ?? user.FullName;
                user.Address = request.Address ?? user.Address;
                user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
                user.RoomId = request.RoomId ?? user.RoomId;
                user.Email = request.Email ?? request.Email;

                if (fileDetail.IsContain)
                {
                    List<FileDetails> images = [fileDetail];

                    List<string> fileName = await uploadImage.UploadImage(images);

                    user.ProfileImage = String.Join(",", fileName);
                }

                user.UpdatedBy = user.Username;
                user.UpdatedAt = DateTimeOffset.Now;

                bool check = await _userRepository.EditAsync(user);

                if (check)
                {
                    response.Status = StatusCode.OK;
                    response.Payload.Message = "Thay đổi thông tin thành công";

                    data.Username = user.Username;
                    data.FullName = user.FullName;
                    data.Address = user.Address;
                    data.PhoneNumber = user.PhoneNumber;
                    data.Email = user.Email;
                    data.RoomId = user.RoomId;
                    data.ProfileImage = user.ProfileImage;
                    data.Role = user.Role.ToString();
                    
                    response.Payload.Data = data;
                }
                else
                {
                    response.Status = StatusCode.BadRequest;
                    response.Payload.Message = "Thay đổi thông tin thất bại";
                    response.Payload.Data = null;
                }
            }
            
            return response;
        }

        public async Task<ResponseObject<CountUserResponse>> CountUser()
        {
            ResponseObject<CountUserResponse> response = new();
            CountUserResponse data = new();

            int count = await _userRepository.Count();
            data.Count = count;

            response.Status = StatusCode.OK;
            response.Payload.Message = "Lấy dữ liệu thành công";
            response.Payload.Data = data;

            return response;
        }

        public async Task<ResponseObject<LoginResponse>> ApprovePartnerRegistration(EditPartnerRegistrationRequest request)
        {
            ResponseObject<LoginResponse> response = new();
            _ = new LoginResponse();
            User? partner = await _userRepository.GetByIdAsync(request.PartnerId);

            if (partner != null)
            {
                /*CareCenter? careCenter = await _careCenterRepository.GetByPartnerId(request.PartnerId);

                if (careCenter != null) {
                    if (careCenter.Manager != null)
                    {
                        User? manager = careCenter.Manager;

                        manager.Status = UserStatus.ACTIVE;
                        partner.Status = UserStatus.ACTIVE;
                        careCenter.Status = CareCenterStatus.ACTIVE;

                        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                        _ = await _careCenterRepository.EditAsync(careCenter);
                        _ = await _userRepository.EditAsync(manager);
                        _ = await _userRepository.EditAsync(partner);

                        scope.Complete();

                        response.Status = StatusCode.OK;
                        response.Payload.Message = "Xác nhận thành công";
                    }
                    else
                    {
                        response.Status = StatusCode.BadRequest;
                        response.Payload.Message = "Không tìm thấy thông tin tài khoản của Quản lý";
                    }
                } 
                else
                {
                    response.Status = StatusCode.BadRequest;
                    response.Payload.Message = "Không tìm thấy thông tin trung tâm chăm sóc";
                }*/
                partner.Status = UserStatus.ACTIVE;

                using TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled);

                _ = await _userRepository.EditAsync(partner);

                scope.Complete();

                response.Status = StatusCode.OK;
                response.Payload.Message = "Xác nhận thành công";
            }
            else
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Id đối tác không tồn tại";
            }

            return response;
        }

        public async Task<ResponseObject<LoginResponse>> RejestPartnerRegistration(EditPartnerRegistrationRequest request)
        {
            ResponseObject<LoginResponse> response = new();
            _ = new LoginResponse();
            User? partner = await _userRepository.GetByIdAsync(request.PartnerId);

            if (partner != null)
            {
                /*CareCenter? careCenter = await _careCenterRepository.GetByPartnerId(request.PartnerId);

                if (careCenter != null)
                {
                    if (careCenter.Manager != null)
                    {
                        User? manager = careCenter.Manager;

                        manager.Status = UserStatus.REJECTED;
                        partner.Status = UserStatus.REJECTED;
                        careCenter.Status = CareCenterStatus.REJECTED;

                        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                        _ = await _careCenterRepository.EditAsync(careCenter);
                        _ = await _userRepository.EditAsync(manager);
                        _ = await _userRepository.EditAsync(partner);

                        scope.Complete();

                        response.Status = StatusCode.OK;
                        response.Payload.Message = "Từ chối thành công";
                    }
                    else
                    {
                        response.Status = StatusCode.BadRequest;
                        response.Payload.Message = "Không tìm thấy thông tin tài khoản của Quản lý";
                    }
                }
                else
                {
                    response.Status = StatusCode.BadRequest;
                    response.Payload.Message = "Không tìm thấy thông tin trung tâm chăm sóc";
                }*/
                partner.Status = UserStatus.REJECTED;

                using TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled);

                _ = await _userRepository.EditAsync(partner);

                scope.Complete();

                response.Status = StatusCode.OK;
                response.Payload.Message = "Từ chối thành công";
            }
            else
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Id đối tác không tồn tại";
            }

            return response;
        }

        public async Task<ResponseObject<ListUserResponse>> GetUser(ListRequest request, UserStatus? status, UserRole? role)
        {
            ResponseObject<ListUserResponse> response = new();
            ListUserResponse data = new();

            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                Search = request.Search ?? string.Empty,
                MaxPage = 1
            };

            Tuple<List<User>, int> list = await _userRepository.GetWithPagingAndStatusAndRole(paging, status, role);
            List<UserModel> modelList = [];

            if (list != null)
            {


                foreach (User item in list.Item1)
                {
                    UserModel model = new()
                    {
                        Id = item.Id,
                        Username = item.Username,
                        FullName = item.FullName,
                        Address = item.Address,
                        Email = item.Email,
                        PhoneNumber = item.PhoneNumber,
                        Role = item.Role
                    };
                    modelList.Add(model);
                }

                response.Status = StatusCode.OK;
                response.Payload.Message = "Lấy dữ liệu thành công";
                data.List = modelList;
                data.Paging = paging;
                data.Paging.MaxPage = list.Item2;
                response.Payload.Data = data;
            }
            else
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Không có bát cứ tài khoản nào";
            }

            return response;
        }

        public async Task<ResponseObject<UserDetailResponse>> GetPendingPartnerById(Guid userId)
        {
            ResponseObject<UserDetailResponse> response = new();
            UserDetailResponse data = new();
            User? user = await _userRepository.GetByIdAsync(userId);

            if (user != null && user.Status == UserStatus.PENDING && user.Role == UserRole.PARTNER)
            {
                response.Status = StatusCode.OK;
                response.Payload.Message = "Truy vấn thông tin thành công";

                data = _mapper.Map<UserDetailResponse>(user);
                response.Payload.Data = data;
            }
            else
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Id người dùng không tồn tại";
                response.Payload.Data = null;
            }

            return response;
        }

        public async Task<ResponseObject<LoginResponse>> CreateStaff(Guid userId, CreateStaffRequest request)
        {
            ResponseObject<LoginResponse> response = new();
            LoginResponse data = new();
            User? user = await _authRepository.GetByUsername(request.Username);
            CareCenter? careCenter = await _careCenterRepository.GetByManagerId(userId);

            if (user != null)
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Đã tồn tại tên tài khoản, xin vui lòng nhập lại";
                response.Payload.Data = null;
            }
            else if (careCenter == null)
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Không thể tìm thấy trung tâm chăm sóc";
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
                    Role = Database.Model.Meta.UserRole.STAFF,
                    CreatedAt = DateTime.UtcNow.AddHours(7),
                    CreatedBy = request.Username
                };

                User? checkUser = await _userRepository.AddAsync(newUser);
                CareCenterStaff careCenterStaff = new CareCenterStaff();
                careCenterStaff.Id = Guid.NewGuid();
                careCenterStaff.UserId = newUser.Id;
                careCenterStaff.CareCenterId = careCenter.Id;
                await _careCenterStaffRepository.AddAsync(careCenterStaff);
            }
            return response;
        }
    }
}
