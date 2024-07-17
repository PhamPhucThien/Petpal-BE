using CapstoneProject.Business.Interface;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.CareCenters;
using CapstoneProject.DTO.Response.CareCenters;
using CapstoneProject.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Business.Service
{
    public class CareCenterService(ICareCenterRepository careCenterRepository) : ICareCenterService
    {
        private readonly ICareCenterRepository _careCenterRepository = careCenterRepository;

        public async Task<GetCareCenterListResponse> GetList(GetCareCenterListRequest request)
        {
            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                MaxPage = 1
            };
            List<CareCenter> list = await _careCenterRepository.GetWithPaging(paging);

            GetCareCenterListResponse response = new();

            response.List = list;
            response.Paging = paging;
            response.Paging.Total = list.Count;

            return response;
        }
    }
}
