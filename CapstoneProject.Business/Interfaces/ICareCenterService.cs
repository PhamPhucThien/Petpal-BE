using CapstoneProject.Database.Model.Meta;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.CareCenters;
using CapstoneProject.DTO.Request.User;
using CapstoneProject.DTO.Response.CareCenters;

namespace CapstoneProject.Business.Interfaces
{
    public interface ICareCenterService
    {
        Task<ResponseObject<EditCareCenterRegistrationResponse>> EditCareCenterRegistration(EditCareCenterRegistrationRequest request, CareCenterStatus careCenterStatus, UserStatus userStatus);
        Task<ResponseObject<CreateCareCenterAndManagerResponse>> CreateCareCenterAndManager(Guid userId, CreateCareCenterRequest request, FileDetails front_image, FileDetails back_image, FileDetails carecenter_image);
        Task<ResponseObject<GetCareCenterListResponse>> GetList(ListRequest request);
        Task<ResponseObject<GetCareCenterListResponse>> GetCareCenterByRole(Guid userId, ListRequest request);
    }
}
