using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Bases
{
    public class Response<T>
    {

        public HttpStatusCode StatusCode { get; set; }
        public Object Meta { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }

        public Response()
        {
            
        }
        public Response(T data, string msg=null)
        {
            Succeeded = true;
            Message = msg;
            Data= data;
        }

        public Response(string msg)
        {
            Succeeded= false;
            Message = msg;
        }

        public Response(string msg, bool succeeded)
        {
            Succeeded= succeeded;
            Message = msg;
        }
    }
}
