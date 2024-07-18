using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneProject.DTO.Request.Account;
using CapstoneProject.DTO.Request.User;
using CapstoneProject.DTO.Response.User;

namespace CapstoneProject.Business.Interface
{
    public interface IUserService
    {
/*        Task<bool> UploadProfile(FileStream file);             
*/    
        Task<UserListResponse> GetList(UserListRequest request);
        Task<UserResponse> GetUserById(string userID);
        Task<UserResponse> CreateUser(UserCreateRequest request);
        Task<UserResponse> UpdateUser(UserUpdateRequest request);
    }
}
