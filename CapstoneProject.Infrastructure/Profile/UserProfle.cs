using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request.User;
using CapstoneProject.DTO.Response.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace CapstoneProject.Infrastructure.Profile
{
    internal class UserProfle : AutoMapper.Profile
    {
        public UserProfle()
        {
            CreateMap<User, UserResponse>();
            CreateMap<UserCreateRequest, User>();
            CreateMap<UserUpdateRequest, User>();
        }
    }
}
