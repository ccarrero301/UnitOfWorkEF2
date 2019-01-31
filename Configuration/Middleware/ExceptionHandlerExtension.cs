namespace Configuration.Middleware
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using InternalServices;
    using Shared.Exceptions;
    using GlobalExceptionHandler.WebApi;
    using Newtonsoft.Json;

    internal static class ExceptionHandlerExtension
    {
        internal static void UseCustomExceptionHandlerBuilder(this IApplicationBuilder applicationBuilder, ILog log)
        {
            HandleGeneralExceptionErrors(applicationBuilder, log);
            HandleUserUnauthenticatedExceptionErrors(applicationBuilder, log);
            HandleSpecificationOrderByExceptionErrors(applicationBuilder, log);
        }

        private static void HandleGeneralExceptionErrors(IApplicationBuilder app, ILog log)
        {
            app.UseGlobalExceptionHandler(x =>
            {
                x.ContentType = "application/json";
                x.ResponseBody(s => JsonConvert.SerializeObject(new
                {
                    Message = "This is the general error"
                }));

                x.Map<Exception>().ToStatusCode(StatusCodes.Status500InternalServerError)
                    .WithBody((ex, context) => JsonConvert.SerializeObject(new
                    {
                        Message = "500 Internal Server Error"
                    }));

                x.OnError((exception, httpContext) =>
                {
                    log.LogException("Exception Found {@data}", exception);

                    return Task.CompletedTask;
                });
            });
        }

        private static void HandleUserUnauthenticatedExceptionErrors(IApplicationBuilder app, ILog log)
        {
            app.UseGlobalExceptionHandler(x =>
            {
                x.ContentType = "application/json";
                x.ResponseBody(s => JsonConvert.SerializeObject(new
                {
                    Message = "This is the general error"
                }));

                x.Map<UnauthenticatedUserException>().ToStatusCode(StatusCodes.Status401Unauthorized)
                    .WithBody((ex, context) => JsonConvert.SerializeObject(new
                    {
                        Message = "User is not authenticated!"
                    }));

                x.OnError((exception, httpContext) =>
                {
                    var unauthenticatedUserException = exception as UnauthenticatedUserException;

                    log.LogException("Exception Found {@data}", exception);

                    if (unauthenticatedUserException != null)
                        log.LogException(
                            $"Authentication attempt with user name {unauthenticatedUserException.AttemptedUser} and password {unauthenticatedUserException.AttemptedPassword}",
                            unauthenticatedUserException);

                    return Task.CompletedTask;
                });
            });
        }

        private static void HandleSpecificationOrderByExceptionErrors(IApplicationBuilder app, ILog log)
        {
            app.UseGlobalExceptionHandler(x =>
            {
                x.ContentType = "application/json";
                x.ResponseBody(s => JsonConvert.SerializeObject(new
                {
                    Message = "This is the general error"
                }));

                x.Map<SpecificationOrderByException>().ToStatusCode(StatusCodes.Status401Unauthorized)
                    .WithBody((ex, context) => JsonConvert.SerializeObject(new
                    {
                        Message = "Order by configuration is not correct!"
                    }));

                x.OnError((exception, httpContext) =>
                {
                    log.LogException("Exception Found {@data}", exception);

                    return Task.CompletedTask;
                });
            });
        }
    }
}