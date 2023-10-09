using System.Net;

namespace Rusada.Core.Common
{
    public class RusdaException : Exception
    {
        public HttpStatusCode HttpErrorCode { get; set; }
        public string ErrorKey { get; set; }
        public object? Model { get; set; }

        public RusdaException(string message, HttpStatusCode httpErrorCode, object model) : base(message)
        {
            Model = model;
            HttpErrorCode = httpErrorCode;
        }

        public RusdaException(string message, HttpStatusCode httpErrorCode) : base(message)
        {
            HttpErrorCode = httpErrorCode;
        }

        public RusdaException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}