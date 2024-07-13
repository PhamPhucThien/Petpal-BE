using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.Account
{
    public class ConfirmAccountRequest
    {
        [Required(ErrorMessage = "User ID is required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "UserName is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; }
        //[Required(ErrorMessage = "ProfileImage is required")]
        public string ProfileImage { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

    }
}
