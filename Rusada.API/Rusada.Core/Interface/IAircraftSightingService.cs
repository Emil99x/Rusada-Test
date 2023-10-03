using Rusada.Core.Dto;

namespace Rusada.Core.Interface
{
    public interface IAircraftSightingService
    {
        Task<AircraftDto> AddSightingAsync(AircraftDto aircraftDto);
        Task<List<AircraftDto>> GetAllAsync();
    }
}