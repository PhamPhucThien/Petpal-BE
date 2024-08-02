using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.CareCenters;
using CapstoneProject.DTO.Request.User;
using CapstoneProject.DTO.Response.CareCenters;

namespace CapstoneProject.Business.Interface
{
    public interface ICareCenterService
    {
        Task<ResponseObject<EditCareCenterRegistrationResponse>> EditCareCenterRegistration(EditCareCenterRegistrationRequest request, CareCenterStatus careCenterStatus, UserStatus userStatus);
        Task<ResponseObject<CreateCareCenterAndManagerResponse>> CreateCareCenterAndManager(Guid userId, CreateCareCenterRequest request);
        Task<ResponseObject<GetCareCenterListResponse>> GetList(GetCareCenterListRequest request);
    }
}
