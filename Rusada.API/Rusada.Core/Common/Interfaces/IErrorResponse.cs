namespace Rusada.Core.Common.Interfaces;

public interface IErrorResponse : IResponse<object>
{
    object Errors { get; init; }
}