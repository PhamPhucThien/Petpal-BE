using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Business
{
    public class StatusCode
    {
        public string OK = "200 " + HttpStatusCode.OK;
        public string BadRequest = "400 " + HttpStatusCode.BadRequest;
        public string Created = "202 " + HttpStatusCode.Created;
        public string NotFound = "404 " + HttpStatusCode.NotFound;
        public string Redirect = "302 " + HttpStatusCode.Redirect;
    }
}
