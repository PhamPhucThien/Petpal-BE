﻿using CapstoneProject.DTO.Request.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.User
{
    public class CreatePartnerRequest
    {
        public RegisterRequest Partner { get; set; } = new();
        public CareCenterModel CareCenter { get; set; } = new();
        public ManagerModel Manager { get; set; } = new();
    }

    public class CareCenterModel {
        public string? CareCenterName { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
    }

    public class ManagerModel
    {
        [Required]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Độ dài tên tài khoản phải từ 8 đến 15 ký tự")]
        public string Username { get; set; } = string.Empty;
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Độ dài mật khẩu phải từ 8 đến 15 ký tự")]
        public string Password { get; set; } = string.Empty;
    }
}
