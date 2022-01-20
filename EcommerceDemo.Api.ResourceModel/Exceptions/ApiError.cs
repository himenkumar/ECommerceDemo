namespace EcommerceDemo.Api.ResourceModel.Exceptions
{
    public class ApiError
    {
        public ApiErrorCode Code { get; }
        public string Message { get; }
        public string Data { get; }
        public ApiError(ApiErrorCode code, string message)
        {
            Code = code;
            Message = message;
        }
        public ApiError(ApiErrorCode code, string message, string data) : this(code, message)
        {
            Data = data;
        }
    }

    public enum ApiErrorCode
    {
        ResourceNotFound,
        CanNotGetResource,
        CanNotCreateResource,
        CanNotUpdateResource,
        CanNotDeleteResource,
    }
}
