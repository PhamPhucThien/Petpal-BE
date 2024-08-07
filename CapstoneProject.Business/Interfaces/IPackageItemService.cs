﻿using CapstoneProject.DTO.Request.Base;
using CapstoneProject.DTO.Request.Package;
using CapstoneProject.DTO.Response.Base;
using CapstoneProject.DTO.Response.Package;

namespace CapstoneProject.Business.Interfaces
{
    public interface IPackageItemService
    {
        Task<BaseListResponse<ListPackageItemResponse>> GetList(ListRequest request);
        Task<ListPackageItemResponse> GetById(string packageItemId);
        Task<ListPackageItemResponse> Create(PackageItemCreateRequest request);
        Task<ListPackageItemResponse> Update(PackageItemUpdateRequest request);
    }
}
