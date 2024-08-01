using CapstoneProject.Business.Interface;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.CareCenters;
using CapstoneProject.DTO.Response.CareCenters;
using CapstoneProject.Repository.Interface;

namespace CapstoneProject.Business.Service
{
    public class CareCenterService(ICareCenterRepository careCenterRepository) : ICareCenterService
    {
        private readonly ICareCenterRepository _careCenterRepository = careCenterRepository;
        public StatusCode StatusCode { get; set; } = new();
        public async Task<ResponseObject<GetCareCenterListResponse>> GetList(GetCareCenterListRequest request)
        {
            ResponseObject<GetCareCenterListResponse> response = new();

            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                MaxPage = 1
            };

            List<CareCenter> list = await _careCenterRepository.GetWithPaging(paging);

            GetCareCenterListResponse listModel = new();

            foreach (CareCenter item in list)
            {
                CareCenterListModel model = new()
                {
                    Address = item.Address,
                    CareCenterName = item.CareCenterName,
                    AverageRating = item.AverageRating,
                    Id = item.Id,
                    ListImages = item.ListImages
                };
                listModel.List.Add(model);
            }

            listModel.Paging = paging;
            listModel.Paging.Total = paging.Total;

            response.Status = StatusCode.OK;
            response.Payload.Data = listModel;

            return response;
        }
    }
}
