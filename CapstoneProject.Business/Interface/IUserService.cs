using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.User;
using CapstoneProject.DTO.Response.Account;
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
        Task<ResponseObject<UserDetailResponse>> UpdateUser(Guid userId, UserUpdateRequest request, List<FileDetails> filesDetail);
        Task<ResponseObject<CountUserResponse>> CountUser();
        Task<ResponseObject<LoginResponse>> ApprovePartnerRegistration(EditPartnerRegistrationRequest request);
        Task<ResponseObject<LoginResponse>> RejestPartnerRegistration(EditPartnerRegistrationRequest request);
        Task<ResponseObject<ListUserResponse>> GetUser(ListRequest request, UserStatus? status, UserRole? role);
        Task<int> UploadProfile(Guid userId, List<FileDetails> filesDetail);
    }
}
