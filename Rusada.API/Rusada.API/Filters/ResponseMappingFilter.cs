using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Rusada.Core.Common.Interfaces;
using Rusada.Core.Common.ResponseTypes;

namespace Rusada.API.Filters;

public class ResponseMappingFilter: IAsyncActionFilter
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public ResponseMappingFilter(ILoggerFactory loggerFactory, ProblemDetailsFactory problemDetailsFactory)
    {
        _loggerFactory = loggerFactory;
        _problemDetailsFactory = problemDetailsFactory;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var executedContext = await next();
        var traceIdentifier = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier;
        if (executedContext.Result is ObjectResult objectResult && objectResult.Value is IResponse response)
        {
            if (response is ErrorResponse errorResponse)
            {

                var error = _problemDetailsFactory.CreateProblemDetails(context.HttpContext, (int)errorResponse.StatusCode,
                    "Unable to complete the request", type: "applicationError");
                error.Extensions["traceId"] = traceIdentifier;
                error.Extensions.Add("errors", errorResponse.Errors);
                executedContext.Result = new ObjectResult(error)
                {
                    StatusCode = (int)errorResponse.StatusCode
                };
                return;
            }
            if (response is SuccessResponse successResponse)
            {
                executedContext.Result = new ObjectResult(successResponse.Data)
                {
                    StatusCode = (int)successResponse.StatusCode
                };
            }

        }
    }
}