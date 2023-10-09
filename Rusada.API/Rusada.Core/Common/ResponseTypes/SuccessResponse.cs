using System.Net;
using Rusada.Core.Common.Interfaces;

namespace Rusada.Core.Common.ResponseTypes;

public class SuccessResponse<TData> : SuccessResponse, IResponse<TData>
{
    public SuccessResponse(object data) : base(data)
    {
    }

    public new TData Data => (TData)base.Data;
}
public class SuccessResponse : ISuccessResponse
{
    public SuccessResponse(object data)
    {
        StatusCode = HttpStatusCode.OK;
        Data = data;
    }

    public HttpStatusCode StatusCode { get; init; }
    public object Data { get; }
}