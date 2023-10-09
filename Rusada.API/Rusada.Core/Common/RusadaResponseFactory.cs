using System.Net;
using Rusada.Core.Common.Interfaces;
using Rusada.Core.Common.ResponseTypes;

namespace Rusada.Core.Common;

public class ResponseFactory
{
    public static IResponse<TData> Success<TData>(TData data)
    {
        return new SuccessResponse<TData>(data);
    }

    public static IResponse Success()
    {
        return new SuccessResponse(null);
    }

    public static IResponse<TData> Error<TData>(object error, HttpStatusCode statusCode, TData data)
    {
        return new ErrorResponse<TData>(error, statusCode, data);
    }

    public static IResponse<TData> Error<TData>(object error, HttpStatusCode statusCode)
    {
        return new ErrorResponse<TData>(error, statusCode, null);
    }

    public static IResponse Error(object error, HttpStatusCode statusCode, object data = null)
    {
        return new ErrorResponse(error, statusCode, data);
    }
}