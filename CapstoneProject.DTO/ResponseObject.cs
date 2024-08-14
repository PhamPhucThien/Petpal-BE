using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO
{
    public class ResponseObject<T> where T : class
    {
        public string Status { get; set; } = string.Empty;
        public Payload<T> Payload { get; set; } = new Payload<T>();
    }

    public class Payload<T> where T : class
    {
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; } 
        public Payload() { }

        public Payload(string message, T? data)
        {
            Message = message;
            Data = data;
        }
    }
}
