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
        Task<GetCareCenterListResponse> GetList(GetCareCenterListRequest request);
    }
}
