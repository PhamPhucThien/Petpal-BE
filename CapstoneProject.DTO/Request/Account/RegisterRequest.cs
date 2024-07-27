using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.Account
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Độ dài tên tài khoản phải từ 8 đến 15 ký tự")]
        public string Username { get; set; } = string.Empty;
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Độ dài mật khẩu phải từ 8 đến 15 ký tự")]
        public string Password { get; set; } = string.Empty;
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Độ dài tên người dùng phải từ 4 đến 50 ký tự")]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [StringLength(300, ErrorMessage = "Độ dài địa chỉ không quá 300 ký tự")]
        public string? Address { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Độ dài số điện thoại phải từ 8 đến 15 ký tự")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Chỉ được nhập số")]
        public string? PhoneNumber { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Sai định dạng gmail")]
        [StringLength(50, ErrorMessage = "Độ dài gmail không quá 50 ký tự")]
        public string? Email { get; set; }
    }
}
