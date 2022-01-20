using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDemo.Core.DtoModel
{
    public class InvalidServiceCallException : Exception
    {
        public InvalidServiceCallException(string message) : base(message)
        {

        }
    }
}
