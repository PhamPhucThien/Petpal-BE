using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneProject.DTO.Request.Account;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.User;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.User;

namespace CapstoneProject.Business.Interface
{
    public interface IUserService
    {
/*        Task<bool> UploadProfile(FileStream file);             
*/    
        Task<BaseListResponse<UserDetailResponse>> GetList(ListRequest request);
        Task<UserDetailResponse> GetUserById(string userID);
        Task<UserDetailResponse> CreateUser(UserCreateRequest request);
        Task<UserDetailResponse> UpdateUser(UserUpdateRequest request);
    }
}
