using CapstoneProject.Database.Model.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Repository.Interface
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid id, UserRole role);
    }
}
