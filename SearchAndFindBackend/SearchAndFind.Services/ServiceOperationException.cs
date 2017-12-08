using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    public class ServiceOperationException: Exception
    {
        public ServiceOperationException(string message) : base(message)
        {
        }

        public ServiceOperationException(string message, Exception e):base(message,e)
        {

        }
    }
}
