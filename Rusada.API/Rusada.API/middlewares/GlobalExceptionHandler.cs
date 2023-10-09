using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;

namespace Rusada.API.middlewares;

public static class GlobalExceptionHandler
{
    public static void UseApiExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var traceIdentifier = Activity.Current?.Id ?? context.TraceIdentifier;
                var loggerFactory = app.ApplicationServices.GetService<ILoggerFactory>();
                var problemDetailsFactory = context.RequestServices.GetServices<ProblemDetailsFactory>().FirstOrDefault();

                ProblemDetails? error;

                if (problemDetailsFactory != null)
                {
                    error = problemDetailsFactory.CreateProblemDetails(context, (int)HttpStatusCode.InternalServerError,
                        "Something went wrongs.Please try again later.", "exception",
                        "Check the logs for details.Use the traceId as a reference.");
                }
                else
                {
                    error = new ProblemDetails
                    {
                        Title = "Something went wrongs.Please try again later.",
                        Type = "exception",
                        Detail = "Check the logs for details. Use the traceId as a reference.",
                        Status = (int)HttpStatusCode.InternalServerError
                    };
                }

                if (string.IsNullOrEmpty(traceIdentifier))
                {
                    traceIdentifier = Guid.NewGuid().ToString();
                }

                error.Extensions["traceId"] = traceIdentifier;

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null && loggerFactory != null)
                {
                    var logger = loggerFactory.CreateLogger("GlobalExceptionHandler");
                    logger.LogError(contextFeature.Error, $"Unhandled Exception . TraceIdentifier:{traceIdentifier}");
                }

                await context.Response.WriteAsync( JsonConvert.SerializeObject(error));
            });
        });
    }
}