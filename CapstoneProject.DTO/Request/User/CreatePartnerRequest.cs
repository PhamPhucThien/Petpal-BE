using CapstoneProject.DTO.Request.Account;
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
        public PaymentModel Payment { get; set; } = new();
    }

    public class IdentityModel
    {
        [Required(ErrorMessage = "Không được bỏ trống số CMND")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Độ dài số CMND phải là 12 số")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Chỉ được nhập số")]
        public string? Number { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống ngày cấp")]
        public string? CreatedAt { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống nơi cấp")]
        public String? CreatedLocation {  get; set; }
    }
    public class CreateCareCenterRequest
    {
        public CareCenterModel CareCenter { get; set; } = new();
        public RegisterRequest Manager { get; set; } = new();
        public IdentityModel ManagerIdentity { get; set; } = new();
    }

    public class PaymentModel
    {
        public string? Name { get; set; }
        public string? AccountNumber { get; set; }
        public string? ExpiryAt { get; set; }
    }

    public class CareCenterModel {
        [Required(ErrorMessage = "Không được bỏ trống tên trung tâm")]
        public string? CareCenterName { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống địa chỉ")]
        public string? Address { get; set; }
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Độ dài số điện thoại phải là 10 số")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Chỉ được nhập số")]
        public string? Hotline { get; set; }
    }
}
