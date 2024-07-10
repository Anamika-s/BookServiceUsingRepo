using Azure;
using Microsoft.AspNetCore.Diagnostics;

namespace BookServiceUsingRepo.Exceptions
{
    public class AppExceptionHandler : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            ErrorResponse response = new ErrorResponse();
            if (exception is DivideByZeroException)
            {
                response = new ErrorResponse()
                {
                    StatusCode = 500,
                    Title = "Cannit divide by 0",
                    ExceptionMessage = exception.Message
                };
            }
            else if (exception is NullReferenceException)
            {
                response = new ErrorResponse()
                {
                    StatusCode = 500,
                    Title = "cannot be null",
                    ExceptionMessage = exception.Message
                };
            }
            else if (exception is Exception)
            {
                response = new ErrorResponse()
                {
                    StatusCode = 500,
                    Title = "cannot be null",
                    ExceptionMessage = exception.Message
                };


            }


            //var response = new ErrorResponse()
            //{
            //    StatusCode = 500,
            //    Title = "something is not correct",
            //    ExceptionMessage = exception.Message
            //};
            //httpContext.Response.WriteAsJsonAsync("some error came in program");
            //httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.WriteAsJsonAsync(response);
            return ValueTask.FromResult(true);
        }
    }
    public class  ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Title { get; set; }
        public string  ExceptionMessage { get; set; }
    }
}
