﻿using CapstoneProject.Database.Model;
using CapstoneProject.DTO.Request.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Business.Interface
{
    public interface IAuthService
    {
        Task<User?> Login(LoginRequest request);
    }
}
