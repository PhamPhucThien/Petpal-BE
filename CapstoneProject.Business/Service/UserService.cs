using CapstoneProject.Business.Interface;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CapstoneProject.Database.Model;
using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.User;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.User;
using CapstoneProject.Repository.Interface;
using User = CapstoneProject.Database.Model.User;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Response.Account;
using CapstoneProject.DTO.Response.Orders;
using CapstoneProject.Repository.Repository;
using System.Transactions;
using Firebase.Storage;
using Newtonsoft.Json;

namespace CapstoneProject.Business.Service
{
    public class UserService(IUserRepository userRepository, ICareCenterRepository careCenterRepository, IMapper mapper) : IUserService
    {     
        private IUserRepository _userRepository = userRepository;
        private ICareCenterRepository _careCenterRepository = careCenterRepository;
        private IMapper _mapper = mapper;
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
               MaxPage = 1
           };
           var listUser = await _userRepository.GetWithPaging(paging);
           var listUserResponse = _mapper.Map<List<UserDetailResponse>>(listUser);
           paging.Total = listUserResponse.Count;
           BaseListResponse<UserDetailResponse> response = new()
           {
               List = listUserResponse,
               Paging = paging,
           };
           return response;
       }

       public async Task<UserDetailResponse> GetUserById(string userID)
       {
           var user = await _userRepository.GetByIdAsync(Guid.Parse(userID));
           if (user == null)
           {
               throw new Exception("Not found User with this id");
               
           }
           var userResponse = _mapper.Map<UserDetailResponse>(user);
           return userResponse;
           
       }

       public async Task<UserDetailResponse> CreateUser(UserCreateRequest request)
       {
           var userCheck = _userRepository.GetUserByUsername(request.Username);
           if (userCheck != null)
           {
               throw new Exception("Username is duplicated.");
           }
           var userCreate = _mapper.Map<User>(request);
           userCreate.Status = UserStatus.ACTIVE;
           userCreate.CreatedBy = request.CreatedBy;
           userCreate.CreatedAt = DateTimeOffset.Now;
           userCreate.Role = UserRole.CUSTOMER;
           var user = await _userRepository.AddAsync(userCreate);
           return _mapper.Map<UserDetailResponse>(user);
       }

       public async Task<UserDetailResponse> UpdateUser(UserUpdateRequest request)
       {
           var userCheck = await _userRepository.GetByIdAsync(Guid.Parse(request.Id));
           if (userCheck == null)
           {
               throw new Exception("Not found User with this id");
           }
           var userUpdate = _mapper.Map<User>(request);
           userUpdate.Username = userCheck.Username;
           userUpdate.Password = userCheck.Password;
           userUpdate.CreatedAt = userCheck.CreatedAt;
           userUpdate.CreatedBy = userCheck.CreatedBy;
           userUpdate.UpdatedAt = DateTimeOffset.Now;
           userUpdate.UpdatedBy = request.UpdatedBy;
           var updateStatus = await _userRepository.EditAsync(userUpdate);
           return updateStatus ? _mapper.Map<UserDetailResponse>(userUpdate) : null;
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
            LoginResponse data = new();
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

                using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

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
            LoginResponse data = new();
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

                using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

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
                MaxPage = 1
            };

            List<User>? list = await _userRepository.GetWithPagingAndStatusAndRole(paging, status, role);
            List<UserModel> modelList = new();

            if (list != null)
            {
                foreach (var item in list)
                {
                    UserModel model = new()
                    {
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
                response.Payload.Data = data;
            } else
            {
                response.Status = StatusCode.BadRequest;
                response.Payload.Message = "Không có bát cứ tài khoản nào";
            }

            return response;
        }

        public async Task<int> UploadProfile(Guid userId, List<FileDetails> filesDetail)
        {
            int count = 0;

            foreach (var file in filesDetail)
            {
                if (file.FileData != null)
                {
                    using var stream = new MemoryStream(file.FileData);

                    var task = new FirebaseStorage("petpal-c6642.appspot.com")
                        .Child(file.FileName)
                        .PutAsync(stream);

                    var downloadUrl = await task;

                    count++;
                }
            }

            return count;
        }
    }
}
