using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Response.Account
{
    public class AccessTokenResponse
    {
        public string TokenType { get; } = "Bearer";
        public string AccessToken { get; init; }
        public long ExpiresIn { get; init; }
    }
}
