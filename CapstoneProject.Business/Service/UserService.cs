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

namespace CapstoneProject.Business.Service
{
    public class UserService : IUserService
    {
        private static string ApiKey = "YOUR_API_KEY";
        private static string Bucket = "your-bucket.appspot.com";
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

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
           userCreate.Status = BaseStatus.ACTIVE;
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
    }
}
