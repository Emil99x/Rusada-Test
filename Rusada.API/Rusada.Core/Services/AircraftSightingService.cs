using Rusada.Core.Common.Interfaces;
using Rusada.Core.Dto;
using Rusada.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rusada.Domain;

namespace Rusada.Core.Services
{
    public class AircraftSightingService : IAircraftSightingService
    {
        private readonly IRusadaDbContext _rusadaDbContext;

        public AircraftSightingService(IRusadaDbContext rusadaDbContext)
        {
            _rusadaDbContext = rusadaDbContext;
        }

        public async Task<AircraftDto> AddSightingAsync(AircraftDto aircraftDto)
        {
            var aircraft = new Aircraft()
            {
                Location = aircraftDto.Location,
                Model = aircraftDto.Model,
                Make = aircraftDto.Make,
                Registration = aircraftDto.Registration,
                DateTime = aircraftDto.DateTime,
            };
            
            _rusadaDbContext.Aircrafts.Add(aircraft);
            await _rusadaDbContext.SaveChangesAsync();

            return aircraftDto;
        }
    }
}