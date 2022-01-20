using EcommerceDemo.Api.ResourceModel.Exceptions;
using System.Net.Http;
using System.Web.Http.Filters;

namespace EcommerceDemo.WebApi
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var apiException = actionExecutedContext.Exception as ApiException;
            
            if (apiException != null)
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(apiException.StatusCode, 
                    new ApiErrorResult(apiException.Error) );
            }

            base.OnException(actionExecutedContext);
        }
    }
}