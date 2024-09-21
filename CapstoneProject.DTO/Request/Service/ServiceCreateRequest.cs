using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.Service
{
    public class ServiceCreateRequest
    {
        [Required(ErrorMessage = "Tên dịch vụ không được bỏ trống")]
        [MaxLength(30, ErrorMessage = "Tên dịch vụ không được quá 30 ký tự")]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100, ErrorMessage = "Mô tả dịch vụ không được quá 100 ký tự")]
        public string Description { get; set; } = string.Empty;
    }
}
