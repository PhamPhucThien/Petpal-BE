using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model.Meta
{
    public enum CareCenterStatus
    {
        [Description("Hoạt động")]
        ACTIVE,
        [Description("Chờ phê duyệt")]
        PENDING,
        [Description("Từ chối")]
        REJECTED,
        [Description("Đã khóa")]
        DISABLE
    }
}
