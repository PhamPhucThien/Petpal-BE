using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Response.Account
{
    public class AccountResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        //public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfileImage { get; set; }
        public string Status { get; set; }
    }
}
