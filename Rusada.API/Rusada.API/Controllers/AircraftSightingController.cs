using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Post(AircraftDto sighting)
        {
            var result = await _aircraftSightingService.AddSightingAsync(sighting);
            return Ok(result);
        }
        
        [HttpPut]
        public async Task<IActionResult> Put([FromForm]AircraftDto sighting, IFormFile? image)
        {
            var result = await _aircraftSightingService.Update(sighting, image);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _aircraftSightingService.GetAllAsync();
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _aircraftSightingService.GetById(id);
            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _aircraftSightingService.DeleteAircraftAsync(id);
            return Ok(result);
        }
    }
}