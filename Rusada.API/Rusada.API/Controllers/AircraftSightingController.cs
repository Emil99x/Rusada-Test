using Microsoft.AspNetCore.Mvc;
using Rusada.Core.Dto;
using Rusada.Core.Interface;

namespace Rusada.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftSightingController : ControllerBase
    {
        private readonly IAircraftSightingService _aircraftSightingService;

        public AircraftSightingController(IAircraftSightingService aircraftSightingService)
        {
            _aircraftSightingService = aircraftSightingService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateSighting(AircraftDto sighting)
        {
            var result = await _aircraftSightingService.AddSightingAsync(sighting);
            return Ok(result);
        }
    }
}