using Microsoft.AspNetCore.Mvc;
using Rusada.Core.Dto;
using Rusada.Core.Interface;

namespace Rusada.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftSightingController : BaseController
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
            return ResponseResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] AircraftDto sighting, IFormFile? image)
        {
            var result = await _aircraftSightingService.UpdateAsync(sighting, image);
            return ResponseResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _aircraftSightingService.GetAllAsync();
            return ResponseResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _aircraftSightingService.GetByIdAsync(id);
            return ResponseResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _aircraftSightingService.DeleteAircraftAsync(id);
            return ResponseResult(result);
        }
    }
}