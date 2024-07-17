using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model.Meta
{
    public enum BaseStatus
    {
        [Description("Hoạt động")]
        ACTIVE,
        [Description("Đã khóa")]
        DISABLE
    }
}
