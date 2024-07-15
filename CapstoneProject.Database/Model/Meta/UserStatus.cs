using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model.Meta
{
    public enum UserStatus
    {
        [Description("Hoat dong")]
        ACTIVE,
        [Description("Khoa")]
        DISABLE
    }
}
