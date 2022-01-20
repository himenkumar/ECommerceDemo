using EcommerceDemo.Api.ResourceModel.Exceptions;
using System;
using System.Net;

namespace EcommerceDemo.Api.ResourceModel.Exceptions
{
    public abstract class ApiException : Exception
    {
        public abstract HttpStatusCode StatusCode { get; }
        public ApiError Error { get; }
        protected ApiException(ApiErrorCode code, string message) : base(message)
        {
            Error = new ApiError(code, message);
        }

    }

    public class NotFoundException : ApiException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
        public NotFoundException(string message = null) : base(ApiErrorCode.ResourceNotFound, message ?? "Requested resource not found.") { }
    }

    public class ForbiddenException : ApiException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.Forbidden;
        public ForbiddenException(ApiErrorCode code, string message = null) : base(code, message ?? "Request forbidden") { }
    }

    public class BadRequestException : ApiException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public BadRequestException(ApiErrorCode code, string message = null) : base(code, message ?? "Bad request") { }
    }
}
