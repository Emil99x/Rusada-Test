using System.Net;

namespace Rusada.Core.Common.Interfaces;

public interface IResponse<TData> : IResponse
{
    TData Data { get; }
}

public interface IResponse
{
    HttpStatusCode StatusCode { get; init; }
}