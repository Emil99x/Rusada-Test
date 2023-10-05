using Microsoft.AspNetCore.Http;
using Rusada.Core.Dto;

namespace Rusada.Core.Interface
{
    public interface IAircraftSightingService
    {
        Task<AircraftDto> AddSightingAsync(AircraftDto aircraftDto);
        Task<AircraftImageDto> GetAircraftImageAsync(Guid key, string filename);
        Task<AircraftDto> Update(AircraftDto aircraftDto, IFormFile? image);
        Task<List<AircraftDto>> GetAllAsync();
        Task<bool> DeleteAircraftAsync(int id);
        Task<AircraftDto> GetById(int id);
    }
}