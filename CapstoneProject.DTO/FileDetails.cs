using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO
{
    public class FileDetails
    {
        public string? FileName { get; set; }
        public string? TempPath { get; set; }
        public byte[]? FileData { get; set; }
    }
}
