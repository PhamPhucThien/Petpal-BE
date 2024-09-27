using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.Account
{
    public class GetAccountByIdRequest
    {
        [Required(ErrorMessage = "Package ID is required")]
        public string Id { get; set; }
    }
}
