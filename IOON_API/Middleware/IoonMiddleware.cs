using Ioon.Application.Common.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;

namespace API.Middleware
{
    public class IoonMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<IoonMiddleware> _logger;

        public IoonMiddleware(RequestDelegate next, ILogger<IoonMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
         {
            try
            {
                await _next(context);

            }
            catch (Exception ex)
            {
               string methodName = new System.Diagnostics.StackTrace(ex).GetFrame(0).GetFileName();
                _logger.LogError(ex,methodName);
            }
        }

        //protected static Task HandleExceptionAsync(HttpContext context, Exception exception)
        //{
        //    HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        //    context.Response.ContentType = "application/json";

        //    var result = string.Empty;

        //    switch (exception)
        //    {
        //        case RepositoryException ex:
        //            statusCode = ex.;
        //            {

        //            }
        //            statusCode = HttpStatusCode.NotFound;
        //            break;
        //    }

        //}
    }
}
