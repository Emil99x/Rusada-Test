using Microsoft.AspNetCore.Mvc;
using Rusada.Core.Common.Interfaces;

namespace Rusada.API.Controllers;

[Produces("application/json")]
public abstract class BaseController : ControllerBase
{
    protected ObjectResult ResponseResult(IResponse response)
    {
        return new ObjectResult(response);
    }
}