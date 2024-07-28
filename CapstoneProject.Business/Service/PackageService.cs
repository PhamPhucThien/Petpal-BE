using CapstoneProject.Business.Interface;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO;
using CapstoneProject.DTO.Request;
using CapstoneProject.DTO.Request.Package;
using CapstoneProject.DTO.Response.CareCenters;
using CapstoneProject.DTO.Response.Package;
using CapstoneProject.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Business.Service
{
    public class PackageService(IPackageRepository packageRepository) : IPackageService
    {
        private readonly IPackageRepository _packageRepository = packageRepository;
        public StatusCode StatusCode { get; set; } = new();
        public async Task<ResponseObject<PackageResponse>> GetById(GetPackageByIdRequest request)
        {
            ResponseObject<PackageResponse> response = new();

            Package? package = await _packageRepository.GetByIdIncludePackageItem(request.Id);
            
            if (package == null)
            {
                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Id không tồn tại";
            } else             {
                response.Status = StatusCode.OK;
                response.Payload.Message = "Lấy dữ liệu thành công";

                PackageResponse packageResponse = new()
                {
                    Id = package.Id,
                    TotalPrice = package.TotalPrice,
                    Duration = package.Duration,
                    Description = package.Description,
                    Type = package.Type
                };

                List<PackageItemResponse> list = [];    

                foreach (var packageItem in package.PackageItems)
                {
                    PackageItemResponse item = new()
                    {
                        Id = packageItem.Id,
                        CurrentPrice = packageItem.CurrentPrice,
                        Detail = packageItem.Detail
                    };
                    list.Add(item);
                }
                packageResponse.Items = list;
                response.Payload.Data = packageResponse;
            }

            return response;
        }

        public async Task<ResponseObject<ListPackageResponse>> GetListByCareCenterId(GetListPackageByCareCenterIdRequest request)
        {
            ResponseObject<ListPackageResponse> response = new();

            Paging paging = new()
            {
                Page = request.Page,
                Size = request.Size,
                MaxPage = 1
            };

            List<Package> list = await _packageRepository.GetWithPagingByCareCenterId(request.CareCenterId, paging);

            if (list == null || list.Count == 0)
            {
                response.Status = StatusCode.NotFound;
                response.Payload.Message = "Dữ liệu không tồn tại";
            }
            else
            {
                ListPackageResponse data = new();
                List<PackageResponse> listModel = [];

                foreach (var item in list)
                {
                    PackageResponse model = new()
                    {
                        Id = item.Id,
                        Type = item.Type,
                        TotalPrice = item.TotalPrice,
                        Duration = item.Duration,
                        Description = item.Description
                    };
                    listModel.Add(model);
                }
                data.Packages.AddRange(listModel);


                response.Status = StatusCode.OK;
                response.Payload.Message = "Lấy dữ liệu thành công";

                data.Paging = paging;
                data.Paging.Total = paging.Total;

                response.Payload.Data = data;
            }

            

            return response;
        }
    }
}
