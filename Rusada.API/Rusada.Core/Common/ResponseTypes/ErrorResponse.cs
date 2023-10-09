using System.Net;
using Rusada.Core.Common.Interfaces;

namespace Rusada.Core.Common.ResponseTypes;

public class ErrorResponse<TData> : ErrorResponse, IResponse<TData>
{
    public ErrorResponse(object errors, HttpStatusCode statusCode, object data) : base(errors, statusCode, data)
    {
    }

    public new TData Data => (TData)base.Data;
}
public class ErrorResponse : IErrorResponse
{
    public ErrorResponse(object errors, HttpStatusCode statusCode, object data)
    {
        Errors = errors;
        StatusCode = statusCode;
        Data = data;
    }

    public HttpStatusCode StatusCode { get; init; }
    public object Data { get; }
    public object Errors { get; init; }
}