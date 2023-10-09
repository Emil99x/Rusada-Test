using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Rusada.Core.Dto;
using Rusada.Core.Interface;
using Rusada.Core.Services;
using Rusada.Infrastructure;

namespace Rusada.Test.Services;

public class AircraftSightingServiceTest
{
    private readonly RusadaDbContext _context;

    public AircraftSightingServiceTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<RusadaDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
        _context = new RusadaDbContext(optionsBuilder.Options);
    }

    [Fact]
    public async Task add_aircraft_sighting_success()
    {
        var data = GetMockSight();
        var aircraftService = GetAircraftSightingService();
        var result = await aircraftService.AddSightingAsync(data);
        _context.Aircrafts.FirstOrDefault(x => x.Id == result.Data.Id).Should().NotBeNull();
        _context.Aircrafts.Where(x => x.Make == data.Make).ToList().Count.Should().Be(1);
    }

    [Fact]
    public async Task update_aircraft_sighting_success()
    {
        var data = GetMockSight();
        var aircraftService = GetAircraftSightingService();
        var result = await aircraftService.AddSightingAsync(data);
        var savedObj = _context.Aircrafts.FirstOrDefault(x => x.Id == result.Data.Id);
        // assert retried object 
        _context.Aircrafts.FirstOrDefault(x => x.Id == result.Data.Id).Should().NotBeNull();

        // change data 
        var updateDto = new AircraftDto()
        {
            Id = savedObj.Id,
            Location = savedObj.Location,
            Model = "Modified",
            Make = savedObj.Make,
            Registration = savedObj.Registration,
            DateTime = savedObj.DateTime
        };

        var resultUpdate = await aircraftService.UpdateAsync(updateDto, null);
        resultUpdate.Data.Should().NotBeNull();
        var updatedRecord = _context.Aircrafts.FirstOrDefault(x => x.Id == result.Data.Id);
        updatedRecord.Model.Should().Be("Modified");
    }

    [Fact]
    public async Task delete_aircraft_sighting_success()
    {
        var data = GetMockSight();
        var aircraftService = GetAircraftSightingService();
        var result = await aircraftService.AddSightingAsync(data);

        _context.Aircrafts.FirstOrDefault(x => x.Id == result.Data.Id).Should().NotBeNull();
        _context.Aircrafts.Where(x => x.Make == data.Make).ToList().Count.Should().Be(1);

        await aircraftService.DeleteAircraftAsync(result.Data.Id);

        _context.Aircrafts.FirstOrDefault(x => x.Id == result.Data.Id && x.Deleted == false).Should().BeNull();
    }

    private IAircraftSightingService GetAircraftSightingService()
    {
        var configuration = A.Fake<IConfiguration>();
        return new AircraftSightingService(_context, configuration);
    }

    private AircraftDto GetMockSight()
    {
        return new AircraftDto()
        {
            Location = "London Gatwick",
            Make = "Boeing”",
            Model = "777-300ER",
            Registration = "G-ERT",
            DateTime = DateTime.Now.AddDays(-1)
        };
    }
}