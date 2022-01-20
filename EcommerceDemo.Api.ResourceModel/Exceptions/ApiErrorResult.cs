using System.Collections.Generic;

namespace EcommerceDemo.Api.ResourceModel.Exceptions
{
    public class ApiErrorResult
    {
        public ApiError Error { get; private set; }

        public ApiErrorResult(ApiError error)
        {
            Error = error;
        }
    }
}
