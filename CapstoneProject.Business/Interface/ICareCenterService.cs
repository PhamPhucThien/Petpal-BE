using CapstoneProject.DTO;
using CapstoneProject.DTO.Request.CareCenters;
using CapstoneProject.DTO.Response.CareCenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Business.Interface
{
    public interface ICareCenterService
    {
        Task<ResponseObject<GetCareCenterListResponse>> GetList(GetCareCenterListRequest request);
    }
}
