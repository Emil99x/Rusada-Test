using Microsoft.AspNetCore.Http;
using Rusada.Core.Common.Interfaces;
using Rusada.Core.Dto;

namespace Rusada.Core.Interface
{
    public interface IAircraftSightingService
    {
        Task<IResponse<AircraftDto>> AddSightingAsync(AircraftDto aircraftDto);
        Task<IResponse<AircraftImageDto>> GetAircraftImageAsync(Guid key, string filename);
        Task<IResponse<AircraftDto>> UpdateAsync(AircraftDto aircraftDto, IFormFile? image);
        Task<IResponse<List<AircraftDto>>> GetAllAsync();
        Task<IResponse<bool>> DeleteAircraftAsync(int id);
        Task<IResponse<AircraftDto>> GetByIdAsync(int id);
    }
}