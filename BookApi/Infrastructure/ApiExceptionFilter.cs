namespace BookApi.Infrastructure
{
    using Book.Domain.Models;
    using BookApi.Extensions;
    using System.Net.Http;
    using System.Text;
    using System.Web.Http.Filters;

    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly string _errorCode;

        public ApiExceptionFilter(string errorCode)
        {
            this._errorCode = errorCode;
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            StringBuilder exceptionMessage = new StringBuilder();

            if (actionExecutedContext?.Exception != null)
            {
                exceptionMessage.Append(actionExecutedContext.Exception.Message);

                if (actionExecutedContext.Exception.InnerException != null)
                {
                    exceptionMessage.Append("\t");
                    exceptionMessage.Append(actionExecutedContext.Exception.InnerException.Message);
                }
            }

            var errorResponse = new BaseError
            {
                ErrorDescription = $"An unhandled exception was thrown by service. Reason {exceptionMessage.ToString()}",
                ErrorCode = this._errorCode
            };

            var apiResponse = new HttpResponseMessage
            {
                Content = new StringContent(errorResponse.JsonSerialize(), Encoding.UTF8, "application/json"),
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ReasonPhrase = "Please contact your administrator"
            };

            actionExecutedContext.Response = apiResponse;
        }
    }
}