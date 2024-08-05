using CapstoneProject.Database.Model.Meta;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.Pet
{
    public class PetCreateRequest
    {
        [Required(ErrorMessage = "Thiếu tên thú cưng")]
        public string? Fullname { get; set; }
        [Required(ErrorMessage = "Thiếu loại thú cưng")]
        public Guid PetTypeId { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Thiếu ngày sinh thú cưng")]
        public DateTimeOffset? Birthday { get; set; }
        [Required(ErrorMessage = "Thiếu cân nặng thú cưng")]
        public double? Weight { get; set; }
        [Required(ErrorMessage = "Thiếu giới tính thú cưng")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Thiếu giống thú cưng")]
        public string? Breed { get; set; }
        [Required(ErrorMessage = "Thiếu thông tin triệt sản thú cưng")]
        public bool? Sterilise { get; set; }
    }
}
