using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.DTO
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public bool AuthenticationError { get; set; }
        
        public Response() {
            Success = true;
        }

        public Response( string message)
        {
            Message = message;
            Success = false;
        }
    }
}
