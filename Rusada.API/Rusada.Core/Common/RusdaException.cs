using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Rusada.Core.Common
{
    public class RusdaException : Exception
    {
        public HttpStatusCode HttpErrorCode { get; set; }
        public string ErrorKey { get; set; }
        public object? Model { get; set; }
    }
}
