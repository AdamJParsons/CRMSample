using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Application.Common.Exceptions
{
    public class CrmApiException : Exception
    {
        public CrmApiException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
            ApiErrors = new List<CrmApiError>();
            ErrorType = CrmErrorType.Error;
        }

        public CrmApiException(string message, HttpStatusCode statusCode, ICollection<CrmApiError> apiErrors)
            : base(message)
        {
            StatusCode = statusCode;
            ApiErrors = apiErrors;
            ErrorType = CrmErrorType.Error;
        }

        public object? Errors { get; set; }

        public HttpStatusCode StatusCode { get; }

        public ICollection<CrmApiError> ApiErrors { get; }

        public CrmErrorType ErrorType { get; set; }
    }
}
