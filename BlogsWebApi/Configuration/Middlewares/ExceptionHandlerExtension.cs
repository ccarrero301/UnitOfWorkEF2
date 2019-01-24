namespace BlogsWebApi.Configuration.Middlewares
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using InternalServices;
    using GlobalExceptionHandler.WebApi;
    using Newtonsoft.Json;

    public static class ExceptionHandlerExtension
    {
        public static void UseCustomExceptionHandlerBuilder(this IApplicationBuilder applicationBuilder, ILog log)
        {
            HandleGeneralExceptionErrors(applicationBuilder, log);
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
    }
}