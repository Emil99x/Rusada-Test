using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rusada.Core.Interface;

namespace Rusada.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftLogoController : BaseController
    {
        private readonly IAircraftSightingService _aircraftSightingService;

        public AircraftLogoController(IAircraftSightingService aircraftSightingService)
        {
            _aircraftSightingService = aircraftSightingService;
        }

        [AllowAnonymous]
        [Route("image/{key}/{fileName}")]
        [HttpGet]
        public async Task<ActionResult> GetClientLogo(Guid key, string fileName)
        {
            var file = await _aircraftSightingService.GetAircraftImageAsync(key, fileName);
            var bytes = Convert.FromBase64String(file.Data.Base64Logo);
            var result = File(bytes, file.Data.ContentType);
            result.EnableRangeProcessing = true;
            result.EntityTag = Microsoft.Net.Http.Headers.EntityTagHeaderValue.Any;
            return result;
        }
    }
}