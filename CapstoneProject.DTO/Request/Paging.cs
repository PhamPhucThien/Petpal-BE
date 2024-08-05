using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request
{
    public class Paging
    {
        [Range(1, Int64.MaxValue)]
        public int Page { get; set; }
        [Range(1, 20)]
        public int Size { get; set; }
        public string Search { get; set; } = string.Empty;
        public int MaxPage { get; set; }
        public int Total { get; set; }
    }
}
