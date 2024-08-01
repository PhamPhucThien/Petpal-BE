using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.CareCenters;
using CapstoneProject.DTO.Response.CareCenters;

namespace CapstoneProject.Business.Interface
{
    public interface ICareCenterService
    {
        Task<ResponseObject<GetCareCenterListResponse>> GetList(GetCareCenterListRequest request);
    }
}
